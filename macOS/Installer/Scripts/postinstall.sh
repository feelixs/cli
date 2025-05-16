#!/bin/bash

LOGFILE="/tmp/ssotme-install.log"

# clear the file
echo "" > $LOGFILE

exec > "$LOGFILE" 2>&1
set -euxo pipefail

echo "[INFO] Starting postinstall script..."
echo "[INFO] Logging to $LOGFILE"

# Resolve actual user's home directory
REAL_USER=$(stat -f "%Su" /dev/console)
REAL_HOME=$(dscl . -read /Users/$REAL_USER NFSHomeDirectory | awk '{print $2}')
TARGETHOMEDIR="$REAL_HOME/.ssotme"

echo "[INFO] Creating target config dir at $TARGETHOMEDIR"
mkdir -p "$TARGETHOMEDIR"

echo "[INFO] Creating symbolic links..."
ln -sf "/Applications/SSoTme/ssotme" "/usr/local/bin/ssotme"
ln -sf "/Applications/SSoTme/aic" "/usr/local/bin/aic"
ln -sf "/Applications/SSoTme/aicapture" "/usr/local/bin/aicapture"

echo "[INFO] Copying dotnet_info.json if present..."
if [ -f "/Applications/SSoTme/dotnet_info.json" ]; then
    cp "/Applications/SSoTme/dotnet_info.json" "$TARGETHOMEDIR/"
    rm "/Applications/SSoTme/dotnet_info.json"
else
    echo "[WARN] dotnet_info.json not found in /Applications/SSoTme"
fi

DOTNET_INFO="$TARGETHOMEDIR/dotnet_info.json"
DOTNET_SCRIPT="$TARGETHOMEDIR/dotnet-install.sh"

echo "[INFO] Checking for curl..."
if ! command -v curl >/dev/null 2>&1; then
    echo "[ERROR] curl is not available"
    exit 1
fi

echo "[INFO] Downloading dotnet install script..."
curl -sSL https://dot.net/v1/dotnet-install.sh -o "$DOTNET_SCRIPT"
chmod +x "$DOTNET_SCRIPT"

if [ ! -f "$DOTNET_INFO" ]; then
    echo "[ERROR] Missing $DOTNET_INFO"
    exit 1
fi

echo "[INFO] Parsing version from $DOTNET_INFO"
THEVERSION=$(grep -o '"using_version": "[^"]*"' "$DOTNET_INFO" | cut -d'"' -f4)

if [ -z "$THEVERSION" ]; then
    echo "[ERROR] Version not found in dotnet_info.json"
    exit 1
fi

echo "[INFO] Installing .NET version $THEVERSION"
"$DOTNET_SCRIPT" --version "$THEVERSION"

echo "[INFO] Postinstall script completed successfully."
exit 0
