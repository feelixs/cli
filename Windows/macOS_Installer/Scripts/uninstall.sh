#!/bin/bash
# Uninstaller script for SSoTme CLI macOS package
echo "Uninstalling SSoTme CLI..."

# Remove symbolic links from /usr/local/bin
sudo rm -f /usr/local/bin/ssotme
sudo rm -f /usr/local/bin/aic
sudo rm -f /usr/local/bin/aicapture

# Remove application files
sudo rm -rf /Applications/SSoTme

echo "Removing configuration files..."
rm -rf ~/.ssotme

echo "SSoTme CLI has been uninstalled successfully."

exit 0