// server.js

const TIMEOUT_SECS = 20;

const http = require('http');
const state = new Map();

const actionSubmissions = new Map();  // bools whether copilot submitted a new action to each base
const actionContents = new Map(); // if its a write action, copilot will populate this with the contents
const requestedActions = new Map(); // strings of the most recent action copilot wants per base
const requestedActionsTableIds = new Map(); // the table id where the action should be run
const requestedActionsFieldIds = new Map(); // the field id where the action should be run

const actionsFinished = new Map();  // bools whether the CLI responded for this base
const actionResponses = new Map();

const basesAvailable = new Map();

const TTL_MS = 60 * 1000;

const validActionEndpoints = ['list-tables', 'update-field', 'get-table-fields', 'create-column'];

// todo connect the user's microsoft account to their ssotme account? To check all the available baseIds for this user
// todo and to make sure they can access a specified baseId
function log(message, data = null) {
  const timestamp = new Date().toISOString();
  console.log(`[${timestamp}] SERVER: ${message}`);
  if (data) {
    console.log(`[${timestamp}] DATA:`, JSON.stringify(data, null, 2));
  }
}

function getActionCliFormat(action) {
  // convert action str to format understandable by cli
  return action.replace("-", "_");
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
  if ((url.pathname !== "/check") && (url.pathname !== '/copilot/check-base') && (url.pathname !== '/copilot/check-req-actions')) {
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

  if (req.method === "GET" && url.pathname === "/copilot/check-req-actions")
      // the cli will check this endpoint to see which actions the plugin wants to do
  {
    const ts = actionSubmissions.get(baseId) || 0;
    const isRecent = (Date.now() - ts) < TTL_MS;
    res.writeHead(200, { "Content-Type": "application/json" });
    actionSubmissions.delete(baseId);
    return res.end(JSON.stringify({
      "changed": isRecent,
      "action": requestedActions.get(baseId),
      "tableId": requestedActionsTableIds.get(baseId),
      "fieldId": requestedActionsFieldIds.get(baseId),
      "content": actionContents.get(baseId),
    }));
    // now the cli should immediatelly post the base's content to /put-action-result
  }

  if (req.method === "POST" && url.pathname === "/copilot/put-action-result")
      // this cli will POST the results of actions here
      // the plugin's rest API (this script) will be polling `actionsFinished` before it returns anything to the plugin, or timeout
  {
    let body = '';
    req.on('data', chunk => {
      body += chunk.toString();
    });
    req.on('end', () => {
      let content;
      let message;
      try {
        const data = JSON.parse(body);
        log(`[PUT-ACTION-RESULT] Raw body received: ${body}`);
        content = data.content || null;
        message = data.msg || null;
        log(`[PUT-ACTION-RESULT] Content received for baseId: ${baseId}`);
      } catch (e) {
        log(`[PUT-ACTION-RESULT] ERROR: invalid JSON for baseId: ${baseId}`);
        res.writeHead(400);
        return res.end(JSON.stringify({'msg': "Invalid JSON"}));
      }

      // responses
      actionsFinished.set(baseId, Date.now());
      if (content) {
        actionResponses.set(baseId, content);
      } else if (message) {
        // fall back to the message we got from the cli
        actionResponses.set(baseId, message);
      }

      // cleanup
      actionContents.delete(baseId);
      requestedActions.delete(baseId);
      requestedActionsTableIds.delete(baseId);
      requestedActionsFieldIds.delete(baseId);

      log(`[PUT-ACTION-RESULT] SUCCESS: stored content for baseId: ${baseId}`);
      res.writeHead(200, { "Content-Type": "application/json" });
      return res.end(JSON.stringify({'msg': `Marked ${baseId} with content`}));
    });
    return;
  }

  if (req.method === "POST" && url.pathname.startsWith("/copilot/do-action/"))
    // valid url mappings: /do-action/list-tables, /do-action/update-field, ... anything in validActionEndpoints
      // copilot will submit an action here
      // & this server will wait until the cli responds and return its response to the plugin (or timeout)
  {
    // Check if baseId exists in available bases for this user
    const userId = getSsotUser(req);
    if (!userId) {
      log(`ERROR: REQUEST-READ missing user parameter`);
      res.writeHead(400, { "Content-Type": "application/json" });
      return res.end(JSON.stringify({'msg': "Missing user parameter"}));
    }

    const desiredAction = url.pathname.split("/").pop();
    if (!validActionEndpoints.includes(desiredAction)) {
      res.writeHead(404, { "Content-Type": "application/json" });
      return res.end(JSON.stringify({'msg': `Action endpoint: '${desiredAction}' not found in ${validActionEndpoints.join(', ')}`}));
    }

    const userBases = basesAvailable.get(userId) || [];
    if (userBases.length > 0 && !userBases.includes(baseId)) {
      log(`ERROR: BaseId '${baseId}' not found in available bases for user ${userId}`);
      res.writeHead(404, { "Content-Type": "application/json" });
      return res.end(JSON.stringify({
        'msg': `Base ID '${baseId}' not found. Available bases: ${userBases.join(', ')}`
      }));
    }

    let body = '';
    req.on('data', chunk => {
      body += chunk.toString();
    });
    req.on('end', async () => {
      let tableid;
      let fieldid;
      let theContent;
      try {
        log(`[ACTION SUBMIT] Raw body received: ${body}`);
        if (!body || body.trim() === '') {
          log(`[ACTION SUBMIT] ERROR: empty body for baseId: ${baseId}`);
          res.writeHead(400);
          return res.end(JSON.stringify({'msg': "Request body is empty", baseId: baseId}));
        }
        const data = JSON.parse(body);
        tableid = data.tableId;
        fieldid = data.fieldId;
        theContent = data.content;
        log(`[ACTION SUBMIT] Parsed data:`, data);
      } catch (e) {
        log(`[ACTION SUBMIT] ERROR: invalid JSON for baseId: ${baseId}, raw body: ${body}`);
        res.writeHead(422);
        return res.end(JSON.stringify({'msg': "Invalid JSON", baseId: baseId}));
      }

      log(`[ACTION SUBMIT] started for baseId: ${baseId}`);
      const reqDate = new Date(Date.now());
      actionSubmissions.set(baseId, reqDate);

      requestedActions.set(baseId, getActionCliFormat(desiredAction));
      requestedActionsTableIds.set(baseId, tableid);
      requestedActionsFieldIds.set(baseId, fieldid);
      actionContents.set(baseId, theContent);

      actionsFinished.delete(baseId);  // clear the last action's result state

      const theTimeout = TIMEOUT_SECS * 1000;
      let waited = 0;
      // the cli will be polling /check-req-actions which returns actionSubmissions[base]
      // copilot calling this endpoint will make /check-req-actions return true

      // then the cli will push to /put-action-result which updates actionsFinished[base] to now()
      let ts = actionsFinished.get(baseId) || 0;
      let newContent = (Date.now() - ts) < TTL_MS;
      log(`Waiting for CLI response on baseId: ${baseId}`);

      try {
        while (!newContent && waited <= theTimeout)
        {
          await new Promise(resolve => setTimeout(resolve, 1000)); // wait 1s
          waited += 1000;

          log(`[ACTION SUBMIT] Polling for CLI response on baseId: ${baseId}`);
          ts = actionsFinished.get(baseId) || 0;
          newContent = (Date.now() - ts) < TTL_MS;
        }

        if (waited > theTimeout) {
          log(`[ACTION SUBMIT] TIMEOUT after ${waited}ms waiting for baseId: ${baseId}`);
          res.writeHead(408);
          return res.end(JSON.stringify({'msg': `Timeout waiting for response from CLI!`, 'baseId': baseId}));
        }
      } catch (error) {
        log(`[ACTION SUBMIT] ERROR in polling loop: ${error.message}`);
        res.writeHead(500);
        return res.end(JSON.stringify({'msg': 'Internal server error', 'baseId': baseId}));
      }

      const updatedContent = actionResponses.get(baseId);
      log(`CLI response received for baseId: ${baseId}`);

      if (!updatedContent) {
        // if it doesn't exist but the cli changed the date... error
        log(`ERROR: No response data found for baseId: ${baseId}`);
        res.writeHead(502);
        return res.end(JSON.stringify({'msg': `Error reading response from CLI! ${baseId}`}));
      }

      log(`Sending successful response to Copilot for baseId: ${baseId}`);
      res.writeHead(200, { "Content-Type": "application/json" });

      return res.end(JSON.stringify({ data: updatedContent, target: {tableId: tableid, baseId: baseId, fieldId: fieldid} }));
    });
    return;
  }

  res.writeHead(404);
  res.end(JSON.stringify({'msg': "Not found"}));
});

server.listen(process.env.PORT || 8080);
