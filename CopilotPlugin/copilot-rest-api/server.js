// server.js
const http = require('http');
const baseLastChanged = new Map();

const readRequests = new Map();  // bools whether copilot wants to read each base
const readAvails = new Map();  // bools whether the CLI responded for this base

const baseContents = new Map(); // the read content of each base returned by the cli
const baseFileNames = new Map();

const baseCmdReqs = new Map(); // copilot will request the cli machine run a command to edit the json file
const baseCmdResps = new Map(); // cli responds to server with the command response, forwarded to copilot

const TTL_MS = 60 * 1000;

// todo - add endpoint to reteive all available bases, that way copilot can resolve simple typos made by the user
function log(message, data = null) {
    const timestamp = new Date().toISOString();
    console.log(`[${timestamp}] SERVER: ${message}`);
    if (data) {
        console.log(`[${timestamp}] DATA:`, JSON.stringify(data, null, 2));
    }
}

const server = http.createServer(async (req, res) => {
    const url = new URL(req.url, `http://${req.headers.host}`);
    const baseId = url.searchParams.get("baseId");
    
    // Log every request not including cli polling
    if ((url.pathname !== '/check-base') && (url.pathname !== '/check-read-req')) {
        log(`${req.method} ${url.pathname} - baseId: ${baseId || 'missing'} - IP: ${req.socket.remoteAddress}`);
    }

    if (!baseId) {
        log(`ERROR: Missing baseId parameter for ${req.method} ${url.pathname}`);
        res.writeHead(400);
        return res.end(JSON.stringify({'msg': "Missing baseId parameter", 'errorCode': 'MISSING_BASE_ID'}));
    }

    if (url.pathname === "/mark-base") {
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
        req.on('end', () => {
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
            let ts = baseCmdResps.get(baseId) || 0;
            let newResp = (Date.now() - ts) < TTL_MS;
            while ((!newResp) && (waited < theTimeout))
            {
                await new Promise(resolve => setTimeout(resolve, 500)); // wait 500ms
                waited += 500;

                log(`Polling for CLI response on baseId: ${baseId}`);
                ts = baseCmdResps.get(baseId) || 0;
                newResp = (Date.now() - ts) < TTL_MS;

                if (waited >= theTimeout) {
                    log(`TIMEOUT (possible cmd failure) waiting for cmd response on baseId: ${baseId}`);
                }
            }
            if (baseCmdResps.has(baseId)) {
                responseMessage = baseCmdResps.get(baseId);
                baseCmdResps.delete(baseId);
            }

            res.writeHead(200, { "Content-Type": "application/json" });
            return res.end(JSON.stringify({'msg': responseMessage, baseId: baseId}));
        });
        return;
    }

    if (req.method === "GET" && url.pathname === "/check-base")
        // used by the CLI to register if the plugin has requested a change via /mark-base
        // -> should poll this and then update the airtable/etc with the requested content
    {
        const ts = baseLastChanged.get(baseId) || 0;
        const changedRecently = (Date.now() - ts) < TTL_MS;
        const content = baseContents.get(baseId) || null;
        const theCmd = baseCmdReqs.get(baseId) || null;
        const filename = baseFileNames.get(baseId) || null;
        res.writeHead(200, { "Content-Type": "application/json" });
        baseLastChanged.delete(baseId);  // the change has now been merged into our memory dict
        baseCmdReqs.delete(baseId);  // only send cmds once
        return res.end(JSON.stringify({ "changed": changedRecently, content, theCmd, filename }));
    }

    if (req.method === "GET" && url.pathname === "/check-read-req")
        // the cli will check this endpoint to see which bases the plugin wants to read from
    {
        const ts = readRequests.get(baseId) || 0;
        const isRecentReadRequest = (Date.now() - ts) < TTL_MS;
        res.writeHead(200, { "Content-Type": "application/json" });
        readRequests.delete(baseId);
        return res.end(JSON.stringify({ "changed": isRecentReadRequest }));
        // now the cli should immediatelly post the base's content to /put-read
    }

    if (req.method === "POST" && url.pathname === "/put-read")
        // this cli will POST ssot reads here
        // the plugin's rest API (this script) will be polling this before it returns anything to the plugin, or timeout
    {
        let body = '';
        req.on('data', chunk => {
            body += chunk.toString();
        });
        req.on('end', () => {
            let content;
            let filename;
            try {
                const data = JSON.parse(body);
                content = data.content;
                filename = data.filename;
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
            baseFileNames.set(baseId, filename);
            baseContents.set(baseId, content);
            log(`SUCCESS: PUT-READ stored content for baseId: ${baseId}`);
            res.writeHead(200, { "Content-Type": "application/json" });
            return res.end(JSON.stringify({'msg': `Marked ${baseId} with content`}));
        });
        return;
    }

    if (req.method === "GET" && url.pathname === "/request-read")
        // copilot will request a ssot read here
        // & this server will wait until the cli responds and return its response to the plugin (or timeout)
    {
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
            await new Promise(resolve => setTimeout(resolve, 500)); // wait 500ms
            waited += 500;

            log(`Polling for CLI response on baseId: ${baseId}`);
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

        const filename = baseFileNames.get(baseId) || null;
        return res.end(JSON.stringify({ data: updatedContent, baseId: baseId, filename: filename }));
    }

    res.writeHead(404);
    res.end(JSON.stringify({'msg': "Not found"}));
});

server.listen(process.env.PORT || 8080);
