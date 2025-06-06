echo "y" | docker system prune --all
docker build --platform=linux/arm64 -t copilotservertest .
# docker push copilotservertest

echo "running server on http://localhost:8080"
docker run -p 8080:8080 --name copilotservertest copilotservertest
