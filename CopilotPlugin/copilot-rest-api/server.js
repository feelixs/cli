// server.js
const http = require('http');
const state = new Map();
const stateContent = new Map();

const readRequests = new Map();  // bools whether copilot wants to read each base
const readAvails = new Map();  // bools whether the CLI responded for this base
const readResponses = new Map(); // the read content of each base returned by the cli

const TTL_MS = 60 * 1000;

const server = http.createServer(async (req, res) => {
    const url = new URL(req.url, `http://${req.headers.host}`);
    const baseId = url.searchParams.get("baseId");

    if (!baseId) {
        res.writeHead(400);
        return res.end("Missing baseId=app123xyz321");
    }

    if (req.method === "POST" && url.pathname === "/mark-base") {
        // mark this ID with the specified content
        // think of a pull request make via this endpoint which the CLI needs to merge into airtable/baserow/etc
        let body = '';
        req.on('data', chunk => {
            body += chunk.toString();
        });
        req.on('end', () => {
            let content;
            try {
                const data = JSON.parse(body);
                content = data.content;
            } catch (e) {
                res.writeHead(400);
                return res.end("Invalid JSON");
            }
            
            if (!content) {
                res.writeHead(400);
                return res.end("Missing required field 'content'");
            }
            
            state.set(baseId, Date.now());
            stateContent.set(baseId, content);
            res.writeHead(200);
            return res.end(`Marked ${baseId} with content: ${content}`);
        });
        return;
    }

    if (req.method === "GET" && url.pathname === "/check-base") {
        const ts = state.get(baseId) || 0;
        const changed = (Date.now() - ts) < TTL_MS;
        const content = stateContent.get(baseId) || null;
        res.writeHead(200, { "Content-Type": "application/json" });
        state.delete(baseId);
        // stateContent.delete(baseId);
        return res.end(JSON.stringify({ changed, content }));
    }

    if (req.method === "GET" && url.pathname === "/check-read-req")
        // the cli will check this endpoint to see which bases the plugin wants to read from
    {
        const ts = readRequests.get(baseId) || 0;
        const changed = (Date.now() - ts) < TTL_MS;
        res.writeHead(200, { "Content-Type": "application/json" });
        readRequests.delete(baseId);
        return res.end(JSON.stringify({ changed }));
        // now the cli should immediatelly post the base's content to /put-read
    }

    if (req.method === "POST" && url.pathname === "/put-read")
        // this cli will post ssot reads here
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
            } catch (e) {
                res.writeHead(400);
                return res.end("Invalid JSON");
            }

            if (!content) {
                res.writeHead(400);
                return res.end("Missing required field 'content'");
            }

            readAvails.set(baseId, Date.now());
            readResponses.set(baseId, content);
            res.writeHead(200);
            return res.end(`Marked ${baseId} with content: ${content}`);
        });
        return;
    }

    if (req.method === "GET" && url.pathname === "/request-read")
        // copilot will request a ssot read, and wait until the cli responds
    {
        const reqDate = new Date(Date.now());
        readRequests.set(baseId, reqDate);

        const theTimeout = 60 * 1000; // 1 minute
        let waited = 0;
        // the cli will be polling check-read-req/ which returns readRequests[base]
        // then the cli should push to /put-read which will update readAvails[base]

        // now wait for the cli to put something there
        let ts = readAvails.get(baseId) || 0;
        let changed = (Date.now() - ts) < TTL_MS;
        while (!changed)
        {
            await new Promise(resolve => setTimeout(resolve, 500)); // wait 500ms
            waited += 500;

            ts = readAvails.get(baseId) || 0;
            changed = (Date.now() - ts) < TTL_MS;

            if (waited > theTimeout)
            {
                res.writeHead(500);
                return res.end(`Timeout waiting for response from CLI! ${baseId}`);
            }
        }

        const response = readResponses.get(baseId);
        if (!response) {
            // if it doesnt exist but the cli changed the date... error
            res.writeHead(500);
            return res.end(`Error reading response from CLI! ${baseId}`);
        }

        res.writeHead(200, { "Content-Type": "application/json" });
        return res.end(JSON.stringify(response));
    }

    res.writeHead(404);
    res.end("Not found");
});

server.listen(process.env.PORT || 8080);
