// server.js
const http = require('http');
const state = new Map();

const TTL_MS = 60 * 1000;

const server = http.createServer((req, res) => {
  const url = new URL(req.url, `http://${req.headers.host}`);
  const baseId = url.searchParams.get("baseId");

  if (!baseId) {
    res.writeHead(400);
    return res.end("Missing baseId=app123xyz321");
  }

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

  res.writeHead(404);
  res.end("Not found");
});

server.listen(process.env.PORT || 8080);
