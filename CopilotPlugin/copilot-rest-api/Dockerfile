FROM node:20-alpine

# Install Python and pip
RUN apk add --no-cache python3 py3-pip

WORKDIR /app

# Copy server files
COPY server.js .
COPY dummy_cli.py .

# Install Python dependencies
RUN pip3 install requests

# Expose the server port
EXPOSE 8080

# Create a startup script that runs both processes
RUN echo '#!/bin/sh' > start.sh && \
    echo 'node server.js &' >> start.sh && \
    echo 'sleep 2' >> start.sh && \
    echo 'python3 dummy_cli.py &' >> start.sh && \
    echo 'wait' >> start.sh && \
    chmod +x start.sh

CMD ["./start.sh"]
