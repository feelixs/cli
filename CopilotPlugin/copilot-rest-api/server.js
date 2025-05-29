// server.js
const http = require('http');
const state = new Map();
const stateContent = new Map();

const TTL_MS = 60 * 1000;

const server = http.createServer((req, res) => {
    const url = new URL(req.url, `http://${req.headers.host}`);
    const baseId = url.searchParams.get("baseId");

    if (!baseId) {
        res.writeHead(400);
        return res.end("Missing baseId=app123xyz321");
    }

    if (req.method === "POST" && url.pathname === "/mark") {
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

    if (req.method === "GET" && url.pathname === "/check") {
        const ts = state.get(baseId) || 0;
        const changed = (Date.now() - ts) < TTL_MS;
        const content = stateContent.get(baseId) || null;
        res.writeHead(200, { "Content-Type": "application/json" });
        state.delete(baseId);
        // stateContent.delete(baseId);
        return res.end(JSON.stringify({ changed, content }));
    }

    res.writeHead(404);
    res.end("Not found");
});

server.listen(process.env.PORT || 8080);
