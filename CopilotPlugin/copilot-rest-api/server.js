// server.js
const http = require('http');
const state = new Map();
const stateContent = new Map();

const readRequests = new Map();  // bools whether copilot wants to read each base
const readResponses = new Map(); // the read content of each base returned by the cli

const TTL_MS = 60 * 1000;

const server = http.createServer((req, res) => {
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
        req.writeHead(500);
        return res.end(JSON.stringify({baseId: "error"}));
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

            readRequests.set(baseId, Date.now());
            readResponses.set(baseId, content);
            res.writeHead(200);
            return res.end(`Marked ${baseId} with content: ${content}`);
        });
        req.writeHead(500);
        return res.end(JSON.stringify({baseId: "error"}));
    }

    if (req.method === "GET" && url.pathname === "/request-read")
        // copilot will request a ssot read, and wait until the cli responds
    {
        const setDate = new Date(Date.now());
        readRequests.set(baseId, setDate);
        const theTimeout = 60 * 1000; // 1 minute
        var waited = 0;

        // now wait for the cli to put something there
        while (readRequests.get(baseId) === setDate)
        {
            wait(500); // wait 5 seconds
            waited += 500;

            if (waited > theTimeout)
            {
                res.writeHead(500);
                return res.end(`Timeout waiting for response from CLI! ${baseId}`);
            }
        }

        if (readResponses.get(baseId) === null) {
            // if it doesnt exist but the cli changed the date... error
            res.writeHead(500);
            return res.end(`Error reading response from CLI! ${baseId}`);
        }

        res.writeHead(200);
        return res.end(JSON.stringify(readResponses.get(baseId)));
    }

    res.writeHead(404);
    res.end("Not found");
});

server.listen(process.env.PORT || 8080);
