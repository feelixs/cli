#!/bin/bash

mkdir -p "$HOME/.ssotme"

# Create symbolic links for the CLI executables
ln -sf "/Applications/SSoTme/ssotme" "/usr/local/bin/ssotme"
ln -sf "/Applications/SSoTme/aic" "/usr/local/bin/aic"
ln -sf "/Applications/SSoTme/aicapture" "/usr/local/bin/aicapture"

# Copy config files
if [ -f "/Applications/SSoTme/dotnet_info.json" ]; then
    cp "/Applications/SSoTme/dotnet_info.json" "$HOME/.ssotme/"
    rm "/Applications/SSoTme/dotnet_info.json"
fi

TARGETHOMEDIR="$HOME/.ssotme"
# install dotnet
if command -v curl >/dev/null 2>&1; then
  # install the script with curl
   curl -sSL https://dot.net/v1/dotnet-install.sh -o "$TARGETHOMEDIR/dotnet-install.sh"
else
   echo "FATAL: Could not install .NET: curl command not available"
   exit 1
fi
chmod +x "$TARGETHOMEDIR/dotnet-install.sh"

$THEVERSION=$(grep -o '"using_version": "[^"]*"' "$TARGETHOMEDIR/dotnet_info.json" | cut -d'"' -f4)

# run the downloaded script to install the right version of dotnet
"$TARGETHOMEDIR/dotnet-install.sh" --version $THEVERSION

exit 0