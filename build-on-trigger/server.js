// server.js
const http = require('http');
const state = new Map();

const baseLastChanged = new Map();

const readRequests = new Map();  // bools whether copilot wants to read each base
const readAvails = new Map();  // bools whether the CLI responded for this base

const baseContents = new Map(); // the read content of each base returned by the cli

const baseCmdReqs = new Map(); // copilot will request the cli machine run a command to edit the json file
const baseCmdResps = new Map(); // cli responds to server with the command response, forwarded to copilot
const baseCmdRespsTimestamps = new Map();

const basesAvailable = new Map();

const TTL_MS = 60 * 1000;

// todo - add endpoint to reteive all available bases, that way copilot can resolve simple typos made by the user

// todo connect the user's microsoft account to their ssotme account? To check all the available baseIds for this user
// todo and to make sure they can access a specified baseId
function log(message, data = null) {
  const timestamp = new Date().toISOString();
  console.log(`[${timestamp}] SERVER: ${message}`);
  if (data) {
    console.log(`[${timestamp}] DATA:`, JSON.stringify(data, null, 2));
  }
}


function getSsotUser(req) {
  // todo it looks like copilot will automatically populate the api call headers with 'X-Microsoft-TenantID' which can be connected to the user's microsoft account
  const microsoftTenantId = req.headers['X-Microsoft-TenantID'];
  return "test";
}


const server = http.createServer(async (req, res) => {
  const url = new URL(req.url, `http://${req.headers.host}`);

  if  (url.pathname === "/copilot/available-bases") {
    if (req.method === "GET") {
      // copilot reqs here
      const userId = getSsotUser(req);
      if (!userId) {
        log(`ERROR: AVAILABLE-BASES missing user parameter`);
        res.writeHead(400, { "Content-Type": "application/json" });
        return res.end(JSON.stringify({'msg': "Missing user parameter"}));
      }

      log(`AVAILABLE-BASES: Request for available bases for user: ${userId}`);

      const userBases = basesAvailable.get(userId) || [];
      if (userBases.length === 0) {
        log(`AVAILABLE-BASES: No bases available for user: ${userId}`);
        res.writeHead(200, { "Content-Type": "application/json" });
        return res.end(JSON.stringify({
          'bases': [],
          'msg': 'No bases available for this user. The CLI may need to be started.'
        }));
      }

      log(`AVAILABLE-BASES: Returning ${userBases.length} bases for user: ${userId}`);
      res.writeHead(200, { "Content-Type": "application/json" });
      return res.end(JSON.stringify({
        'bases': userBases,
        'msg': 'Available bases retrieved successfully'
      }));
    }
    else if (req.method === "POST") {
      // cli posts here
      // cli could just call this when the process begins, or whatever works best
      let body = '';
      req.on('data', chunk => {
        body += chunk.toString();
      });
      req.on('end', () => {
        try {
          const data = JSON.parse(body);
          const userId = data.user;
          const userBases = data.bases || [];

          if (!userId) {
            log(`ERROR: AVAILABLE-BASES missing user parameter`);
            res.writeHead(400, { "Content-Type": "application/json" });
            return res.end(JSON.stringify({'msg': "Missing user parameter"}));
          }

          basesAvailable.set(userId, userBases);
          log(`AVAILABLE-BASES: CLI registered ${userBases.length} bases for user ${userId}: ${userBases.join(', ')}`);
          res.writeHead(200, { "Content-Type": "application/json" });
          return res.end(JSON.stringify({"msg": `Successfully set available bases for user ${userId}.`}));
        } catch (e) {
          log(`ERROR: AVAILABLE-BASES invalid JSON: ${body}`);
          res.writeHead(400, { "Content-Type": "application/json" });
          return res.end(JSON.stringify({'msg': "Invalid JSON in request body"}));
        }
      })
      return;
    }
  }

  const baseId = url.searchParams.get("baseId");
  // Only check for baseId if it's not the available-bases endpoint
  if (!baseId && url.pathname !== "/copilot/available-bases") {
    log(`ERROR: Missing baseId parameter for ${req.method} ${url.pathname}`);
    res.writeHead(400);
    return res.end(JSON.stringify({'msg': "Missing baseId parameter", 'errorCode': 'MISSING_BASE_ID'}));
  }

  // Log every request not including cli polling
  if ((url.pathname !== "/check") && (url.pathname !== '/copilot/check-base') && (url.pathname !== '/copilot/check-read-req')) {
    log(`${req.method} ${url.pathname} - baseId: ${baseId || 'missing'} - IP: ${req.socket.remoteAddress}`);
  }

  // MARK START original server
  if (req.method === "GET" && url.pathname === "/mark") {
    state.set(baseId, Date.now());
    res.writeHead(200);
    return res.end("Marked");
  }

  if (req.method === "GET" && url.pathname === "/check") {
    const ts = state.get(baseId) || 0;
    const changed = (Date.now() - ts) < TTL_MS;
    res.writeHead(200, { "Content-Type": "application/json" });
    state.delete(baseId);
    return res.end(JSON.stringify({ changed }));
  }
  // MARK END original server

  if (url.pathname === "/copilot/mark-base") {
    if (req.method !== "POST") {
      res.writeHead(400, { "Content-Type": "application/json" });
      return res.end(JSON.stringify({'msg': `Must use POST request for this endpoint!`, baseId: baseId}));
    }

    // mark this ID with the specified content (used by the copilot plugin to request changes to a base)
    // think of a pull request make via this endpoint which the CLI needs to merge into airtable/baserow/etc
    let body = '';
    let responseMessage = "Successfully marked base with new content"; // for overwrite requests from copilot
    req.on('data', chunk => {
      body += chunk.toString();
    });
    req.on('end', async () => {
      let content;
      let command;
      try {
        const data = JSON.parse(body);
        content = data.content;
        command = data.command;
        log(`MARK-BASE: Raw body received: ${body}`);
        log(`MARK-BASE: Parsed data:`, data);
        log(`MARK-BASE: Extracted content:`, content);
        log(`MARK-BASE: Received command: ${command}`);
        log(`MARK-BASE: Valid JSON received for baseId: ${baseId}`);
      } catch (e) {
        log(`ERROR: MARK-BASE invalid JSON for baseId: ${baseId}, raw body: ${body}`);
        res.writeHead(422);
        return res.end(JSON.stringify({'msg': "Invalid JSON", baseId: baseId}));
      }

      if ((!content) && (!command)) {
        log(`ERROR: MARK-BASE missing content for baseId: ${baseId}`);
        res.writeHead(422);
        return res.end(JSON.stringify({'msg': "Either the field 'content' or 'command' are required", baseId: baseId}));
      }

      baseLastChanged.set(baseId, Date.now());
      baseContents.set(baseId, content);
      baseCmdReqs.set(baseId, command);
      log(`SUCCESS: MARK-BASE stored content for baseId: ${baseId}`);
      log(`SUCCESS: MARK-BASE stored command for baseId: ${baseId}`);
      log(`Waiting for cli command response for baseId: ${baseId}`);

      let waited = 0;
      const theTimeout = 30 * 1000;
      let ts = baseCmdRespsTimestamps.get(baseId) || 0;
      let newResp = (Date.now() - ts) < TTL_MS;
      while ((!newResp) && (waited < theTimeout))
      {
        await new Promise(resolve => setTimeout(resolve, 1000)); // wait 1s
        waited += 1000;

        log(`[MARK-BASE] Polling for CLI response on baseId: ${baseId}`);
        ts = baseCmdRespsTimestamps.get(baseId) || 0;
        newResp = (Date.now() - ts) < TTL_MS;

        if (waited >= theTimeout) {
          log(`TIMEOUT (possible cmd failure) waiting for cmd response on baseId: ${baseId}`);
        }
      }
      if (baseCmdResps.has(baseId)) {
        responseMessage = baseCmdResps.get(baseId);
        baseCmdResps.delete(baseId);
        baseCmdRespsTimestamps.delete(baseId);
      }

      res.writeHead(200, { "Content-Type": "application/json" });
      return res.end(JSON.stringify({'msg': responseMessage, baseId: baseId}));
    });
    return;
  }

  if (req.method === "GET" && url.pathname === "/copilot/check-base")
      // used by the CLI to register if the plugin has requested a change via /mark-base
      // -> should poll this and then update the airtable/etc with the requested content
  {
    const ts = baseLastChanged.get(baseId) || 0;
    const changedRecently = (Date.now() - ts) < TTL_MS;
    const content = baseContents.get(baseId) || null;
    const theCmd = baseCmdReqs.get(baseId) || null;
    res.writeHead(200, { "Content-Type": "application/json" });
    baseLastChanged.delete(baseId);  // the change has now been merged into our memory dict
    baseCmdReqs.delete(baseId);  // only send cmds once
    return res.end(JSON.stringify({ "changed": changedRecently, content, theCmd }));
  }

  if (req.method === "GET" && url.pathname === "/copilot/check-read-req")
      // the cli will check this endpoint to see which bases the plugin wants to read from
  {
    const ts = readRequests.get(baseId) || 0;
    const isRecentReadRequest = (Date.now() - ts) < TTL_MS;
    res.writeHead(200, { "Content-Type": "application/json" });
    readRequests.delete(baseId);
    return res.end(JSON.stringify({ "changed": isRecentReadRequest }));
    // now the cli should immediatelly post the base's content to /put-read
  }

  if (req.method === "POST" && url.pathname === "/copilot/put-read")
      // this cli will POST ssot reads here
      // the plugin's rest API (this script) will be polling this before it returns anything to the plugin, or timeout
  {
    let body = '';
    req.on('data', chunk => {
      body += chunk.toString();
    });
    req.on('end', () => {
      let content;
      try {
        const data = JSON.parse(body);
        content = data.content;
        log(`PUT-READ: Content received for baseId: ${baseId}`);
      } catch (e) {
        log(`ERROR: PUT-READ invalid JSON for baseId: ${baseId}`);
        res.writeHead(400);
        return res.end(JSON.stringify({'msg': "Invalid JSON"}));
      }

      if (!content) {
        log(`ERROR: PUT-READ missing content for baseId: ${baseId}`);
        res.writeHead(400);
        return res.end(JSON.stringify({'msg': "Missing required field 'content'"}));
      }

      readAvails.set(baseId, Date.now());
      baseContents.set(baseId, content);
      log(`SUCCESS: PUT-READ stored content for baseId: ${baseId}`);
      res.writeHead(200, { "Content-Type": "application/json" });
      return res.end(JSON.stringify({'msg': `Marked ${baseId} with content`}));
    });
    return;
  }

  if (req.method === "GET" && url.pathname === "/copilot/request-read")
      // copilot will request a ssot read here
      // & this server will wait until the cli responds and return its response to the plugin (or timeout)
  {
    // Check if baseId exists in available bases for this user
    const userId = getSsotUser(req);
    if (!userId) {
      log(`ERROR: REQUEST-READ missing user parameter`);
      res.writeHead(400, { "Content-Type": "application/json" });
      return res.end(JSON.stringify({'msg': "Missing user parameter"}));
    }

    const userBases = basesAvailable.get(userId) || [];
    if (userBases.length > 0 && !userBases.includes(baseId)) {
      log(`ERROR: BaseId '${baseId}' not found in available bases for user ${userId}`);
      res.writeHead(404, { "Content-Type": "application/json" });
      return res.end(JSON.stringify({
        'msg': `Base ID '${baseId}' not found. Available bases: ${userBases.join(', ')}`
      }));
    }

    log(`READ REQUEST started for baseId: ${baseId}`);
    const reqDate = new Date(Date.now());
    readRequests.set(baseId, reqDate);

    const theTimeout = 30 * 1000;
    let waited = 0;
    // the cli will be polling /check-read-req which returns readRequests[base]
    // copilot calling this endpoint will make /check-read-req return true

    // then the cli will push to /put-read which updates readAvails[base] to now()
    let ts = readAvails.get(baseId) || 0;
    let newContent = (Date.now() - ts) < TTL_MS;
    log(`Waiting for CLI response on baseId: ${baseId}`);

    while (!newContent)
    {
      await new Promise(resolve => setTimeout(resolve, 1000)); // wait 1s
      waited += 1000;

      log(`[REQUEST-READ] Polling for CLI response on baseId: ${baseId}`);
      ts = readAvails.get(baseId) || 0;
      newContent = (Date.now() - ts) < TTL_MS;

      if (waited > theTimeout)
      {
        log(`TIMEOUT after ${waited}ms waiting for baseId: ${baseId}`);
        res.writeHead(408);
        return res.end(JSON.stringify({'msg': `Timeout waiting for response from CLI! ${baseId}`}));
      }
    }

    const updatedContent = baseContents.get(baseId);
    log(`CLI response received for baseId: ${baseId}`);

    if (!updatedContent) {
      // if it doesn't exist but the cli changed the date... error
      log(`ERROR: No response data found for baseId: ${baseId}`);
      res.writeHead(502);
      return res.end(JSON.stringify({'msg': `Error reading response from CLI! ${baseId}`}));
    }

    log(`Sending successful response to Copilot for baseId: ${baseId}`);
    res.writeHead(200, { "Content-Type": "application/json" });

    return res.end(JSON.stringify({ data: updatedContent, baseId: baseId }));
  }

  res.writeHead(404);
  res.end(JSON.stringify({'msg': "Not found"}));
});

server.listen(process.env.PORT || 8080);
