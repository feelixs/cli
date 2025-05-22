// server.js
const http = require('http');
const state = new Map();

const TTL_MS = 60 * 1000;

const server = http.createServer((req, res) => {
  const url = new URL(req.url, `http://${req.headers.host}`);
  const appid = url.searchParams.get("appid");

  if (!appid) {
    res.writeHead(400);
    return res.end("Missing appid");
  }

  if (req.method === "GET" && url.pathname === "/mark") {
    state.set(appid, Date.now());
    res.writeHead(200);
    return res.end("Marked");
  }

  if (req.method === "GET" && url.pathname === "/check") {
    const ts = state.get(appid) || 0;
    const changed = (Date.now() - ts) < TTL_MS;
    res.writeHead(200, { "Content-Type": "application/json" });
    return res.end(JSON.stringify({ changed }));
  }

  res.writeHead(404);
  res.end("Not found");
});

server.listen(process.env.PORT || 8080);
