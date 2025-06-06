// server.js

const TIMEOUT_SECS = 20;

const http = require('http');
const baseMap = new Map();


/*

  one action will be like:
  {
      action: "list_tables",
      content: "",
      tableId: "",
      rowId: "",
      fieldId: ""
  }

 */
class BaseState {
  // a bunch of these classes will populate a map: {'baseid1': BaseState(), 'baseid2': BaseState(), ...}
  constructor() {
    this.actionQueue = []; // unified FIFO queue for each base
    this.actionsFinished = 0;  // timestamp
    this.actionResponses = null;
  }

  // Add an action to the FIFO queue
  enqueueAction(action, maxSize = 20) {
    this.actionQueue.push(action);
    if (this.actionQueue.length > maxSize) {
      this.actionQueue.shift(); // keep it trimmed
    }
  }

  // Get the first action without removing it
  peekAction(defaultValue = null) {
    if (!Array.isArray(this.actionQueue) || this.actionQueue.length === 0) {
      return defaultValue;
    }
    return this.actionQueue[0];
  }

  // Remove and return the first action
  dequeueAction() {
    if (!Array.isArray(this.actionQueue) || this.actionQueue.length === 0) {
      return null;
    }
    return this.actionQueue.shift();
  }
}

function getOrInitBase(baseId) {
  if (!baseMap.has(baseId)) {
    baseMap.set(baseId, new BaseState());
  }
  return baseMap.get(baseId);
}

const basesAvailable = new Map();

const TTL_MS = 60 * 1000;

// should match the list defined in the cli
const validActionEndpoints = ['list_tables', 'update_cell', 'get_cell', 'get_table_fields', 'create_field'];

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
  
  const baseId = url.searchParams.get("baseId");
  // Only check for baseId if it's not the available-bases endpoint
  if (!baseId && url.pathname !== "/copilot/available-bases") {
    log(`ERROR: Missing baseId parameter for ${req.method} ${url.pathname}`);
    res.writeHead(400);
    return res.end(JSON.stringify({'msg': "Missing baseId parameter", 'errorCode': 'MISSING_BASE_ID'}));
  }

  const userId = getSsotUser(req);
  if (!userId) {
    log(`ERROR: missing user parameter`);
    res.writeHead(400, { "Content-Type": "application/json" });
    return res.end(JSON.stringify({'msg': "Missing 'X-Microsoft-TenantID' parameter in headers"}));
  }
  
  if  (url.pathname === "/copilot/available-bases") {
    if (req.method === "GET") {
      // copilot reqs here

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

          // convert the received {'bases': [{'id': 1}, ...]} to a list of base id strings
          const basesData = data.bases || []
          const userBases = basesData.map(base => base.id.toString());

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

  // Log every request not including cli polling
  if ((url.pathname !== "/check") && (url.pathname !== '/copilot/check-base') && (url.pathname !== '/copilot/check-req-actions')) {
    log(`${req.method} ${url.pathname} - baseId: ${baseId || 'missing'} - IP: ${req.socket.remoteAddress}`);
  }

  if (req.method === "GET" && url.pathname === "/copilot/check-req-actions") {
    const state = getOrInitBase(baseId);
    const next = state.peekAction();
    const isRecent = next && (Date.now() - next.timestamp < TTL_MS);
    if (isRecent) {
      // Remove the action now that CLI has fetched it
      state.dequeueAction();
    }

    res.writeHead(200, { "Content-Type": "application/json" });
    return res.end(JSON.stringify({
      changed: isRecent,
      action: next?.action || null,
      tableId: next?.tableId || null,
      rowId: next?.rowId || null,
      fieldId: next?.fieldId || null,
      content: next?.content || null
    }));
  }

  if (req.method === "POST" && url.pathname === "/copilot/put-action-result")
      // this cli will POST the results of actions here
      // the plugin's rest API (this script) will be polling finished action before it returns anything to the plugin, or timeout
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
        res.writeHead(400, { "Content-Type": "application/json" });
        return res.end(JSON.stringify({ msg: "Invalid JSON" }));
      }
      const base = getOrInitBase(baseId);
      // store result in base state
      base.actionsFinished = Date.now();
      base.actionResponses = content ?? message ?? null;

      log(`[PUT-ACTION-RESULT] SUCCESS: stored content for baseId: ${baseId}`);
      res.writeHead(200, { "Content-Type": "application/json" });
      return res.end(JSON.stringify({ msg: `Marked ${baseId} with content` }));
    });
    return;
  }

  if (req.method === "POST" && url.pathname.startsWith("/copilot/do-action/"))
    // valid url mappings: /do-action/list-tables, /do-action/update-field, ... anything in validActionEndpoints
      // copilot will submit an action here
      // & this server will wait until the cli responds and return its response to the plugin (or timeout)
  {
    const desiredAction = url.pathname.split("/").pop();
    if (!validActionEndpoints.includes(desiredAction)) {
      res.writeHead(404, { "Content-Type": "application/json" });
      return res.end(JSON.stringify({
        msg: `Action endpoint '${desiredAction}' not found. Valid: ${validActionEndpoints.join(', ')}`
      }));
    }

    const userBases = basesAvailable.get(userId) || [];
    if (userBases.length > 0 && !userBases.includes(baseId)) {
      log(`ERROR: BaseId '${baseId}' not found in available bases for user ${userId}`);
      res.writeHead(404, { "Content-Type": "application/json" });
      return res.end(JSON.stringify({
        msg: `Base ID '${baseId}' not found for the user: '${userId}'. Available bases: ${userBases.join(', ')}`
      }));
    }

    let body = '';
    req.on('data', chunk => {
      body += chunk.toString();
    });
    req.on('end', async () => {
      let tableId, rowId, fieldId, content;
      try {
        log(`[ACTION SUBMIT] Raw body received: ${body}`);
        const data = body.trim() === '' ? {} : JSON.parse(body);
        tableId = data.tableId || null;
        rowId = data.rowId || null;
        fieldId = data.fieldId || null;
        content = data.content || null;
        log(`[ACTION SUBMIT] Parsed data:`, data);
      } catch (e) {
        log(`[ACTION SUBMIT] ERROR: invalid JSON for baseId: ${baseId}, raw body: ${body}`);
        res.writeHead(422);
        return res.end(JSON.stringify({ msg: "Invalid JSON", baseId }));
      }

      const base = getOrInitBase(baseId);
      const reqDate = Date.now();

      log(`[ACTION SUBMIT] Queued '${desiredAction}' for baseId: ${baseId}`);

      // Enqueue the full action object
      base.enqueueAction({
        action: desiredAction,
        content: content,
        tableId: tableId,
        rowId: rowId,
        fieldId: fieldId,
        timestamp: reqDate
      });

      // clear the last action's result state
      base.actionsFinished = 0;
      base.actionResponses = null;

      // Wait for CLI to respond
      const theTimeout = TIMEOUT_SECS * 1000;
      let waited = 0;

      let ts = base.actionsFinished || 0;
      let newContent = (Date.now() - ts) < TTL_MS;
      log(`Waiting for CLI response on baseId: ${baseId}`);

      try {
        while (!newContent && waited <= theTimeout)
        {
          await new Promise(resolve => setTimeout(resolve, 1000)); // wait 1s
          waited += 1000;
          log(`[ACTION SUBMIT] Polling for CLI response on baseId: ${baseId}`);
          ts = base.actionsFinished || 0;
          newContent = (Date.now() - ts) < TTL_MS;
        }

        if (waited > theTimeout) {
          log(`[ACTION SUBMIT] TIMEOUT after ${waited}ms waiting for baseId: ${baseId}`);
          res.writeHead(408);
          return res.end(JSON.stringify({
            msg: `Timeout waiting for response from CLI!`,
            baseId
          }));
        }
      } catch (error) {
        log(`[ACTION SUBMIT] ERROR in polling loop: ${error.message}`);
        res.writeHead(500);
        return res.end(JSON.stringify({
          msg: 'Internal server error',
          baseId
        }));
      }

      const updatedContent = base.actionResponses;
      log(`CLI response received for baseId: ${baseId}`);

      if (!updatedContent) {
        log(`ERROR: No response data found for baseId: ${baseId}`);
        res.writeHead(502);
        return res.end(JSON.stringify({
          msg: `Error reading response from CLI! ${baseId}`
        }));
      }

      log(`Sending successful response to Copilot for baseId: ${baseId}`);
      res.writeHead(200, { "Content-Type": "application/json" });

      return res.end(JSON.stringify({
        data: updatedContent,
        target: {
          baseId,
          tableId,
          rowId,
          fieldId
        }
      }));
    });
    return;
  }

  res.writeHead(404);
  res.end(JSON.stringify({'msg': "Not found"}));
});

server.listen(process.env.PORT || 8080);
