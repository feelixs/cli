#!/bin/bash
# Uninstaller script for SSoTme CLI macOS package

# Check if keep config parameter was passed
KEEP_CONFIG="n"
if [ $# -eq 1 ]; then
    KEEP_CONFIG=$1
fi

echo "Uninstalling SSoTme CLI..."

# Remove symbolic links from /usr/local/bin
sudo rm -f /usr/local/bin/ssotme
sudo rm -f /usr/local/bin/aic
sudo rm -f /usr/local/bin/aicapture

# Remove application files
sudo rm -rf /Applications/SSoTme

# Check whether to remove config files
if [[ ! $KEEP_CONFIG =~ ^[Yy]$ ]]; then
    echo "Removing configuration files..."
    rm -rf ~/.ssotme
else
    echo "Keeping configuration files in ~/.ssotme"
fi

echo "SSoTme CLI has been uninstalled successfully."

# Also remove the manager app itself
sudo rm -rf "/Applications/SSoTme CLI Manager.app"

exit 0