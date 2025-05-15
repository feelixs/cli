#!/bin/bash
# Build script for SSoTme macOS Installer
#
# Generates:
#           - dist/SSoTme-Installer.pkg - macOS installer package


THE_INSTALLER_FILENAME=$1
DEV_INSTALLER_KEYCHAIN_ID=$2
DEV_EXECUTABLE_KEYCHAIN_ID=$3
APPLE_EMAIL=$4
NOTARYPASS=$5

INSTALLER_DIR="$( dirname "$( dirname "${BASH_SOURCE[0]}" )")"

echo "my dir: $INSTALLER_DIR"

set -e  # exit on failure

SCRIPT_DIR="$INSTALLER_DIR/Scripts"

ROOT_DIR="$(dirname "$(dirname "$INSTALLER_DIR")")"

echo "Root dir: $ROOT_DIR"

cd $ROOT_DIR

SOURCE_DIR="$ROOT_DIR/ssotme"
RESOURCES_DIR="$INSTALLER_DIR/Resources"
ASSETS_DIR="$INSTALLER_DIR/Assets"
BUILD_DIR="$INSTALLER_DIR/build"
DIST_DIR="$ROOT_DIR/dist"
BIN_DIR="$INSTALLER_DIR/bin"
SSOTME_DIR="$HOME/.ssotme"
SSOTME_VERSION=$(grep -o '"version": "[^"]*"' "$ROOT_DIR/package.json" | cut -d'"' -f4)


echo "Using version: $SSOTME_VERSION from package.json"

# Clean previous builds
sudo rm -rf "$DIST_DIR"
sudo rm -rf "$BUILD_DIR"
sudo rm -rf "$BIN_DIR"
sudo rm -rf "$ROOT_DIR/build"

echo "Creating necessary directories..."
mkdir -p "$RESOURCES_DIR" "$BUILD_DIR" "$DIST_DIR" "$ASSETS_DIR" "$BIN_DIR"

# Copy README into Resources
README_SRC="$ROOT_DIR/README.md"
README_DEST="$RESOURCES_DIR/README.md"
if [ -f "$README_SRC" ]; then
    cp "$README_SRC" "$README_DEST"
else
    echo "WARNING: README.md not found at root."
fi

echo "Building cli.py..."
/bin/bash "$SOURCE_DIR/build-cli.sh"

echo "Copy executable file ssotme into Resources under aliases: ssotme, aic, aicapture..."
cp "$DIST_DIR/ssotme" "$RESOURCES_DIR/ssotme"
cp "$DIST_DIR/ssotme" "$RESOURCES_DIR/aic"
cp "$DIST_DIR/ssotme" "$RESOURCES_DIR/aicapture"

# sign the executables
codesign --force --timestamp --options runtime \
  --entitlements "$SOURCE_DIR/entitlements.plist" \
  --sign "$DEV_EXECUTABLE_KEYCHAIN_ID" "$RESOURCES_DIR/ssotme" --identifier "com.effortlessapi.ssotme"

codesign --force --timestamp --options runtime \
  --entitlements "$SOURCE_DIR/entitlements.plist" \
  --sign "$DEV_EXECUTABLE_KEYCHAIN_ID" "$RESOURCES_DIR/aic" --identifier "com.effortlessapi.aic"

codesign --force --timestamp --options runtime \
  --entitlements "$SOURCE_DIR/entitlements.plist" \
  --sign "$DEV_EXECUTABLE_KEYCHAIN_ID" "$RESOURCES_DIR/aicapture" --identifier "com.effortlessapi.aicapture"

# this will exist because we just ran the python build (setup.py will generate this json in the home directory)
if [ -f "$SSOTME_DIR/dotnet_info.json" ]; then
    cp "$SSOTME_DIR/dotnet_info.json" "$RESOURCES_DIR/dotnet_info.json"
else
    echo "$SSOTME_DIR/dotnet_info.json does not exist (pyinstaller should have generated this!)"
fi

# copy the postinstall script to the build
mkdir -p "$BUILD_DIR/scripts"
if [ -f "$SCRIPT_DIR/postinstall.sh" ]; then
    cp "$SCRIPT_DIR/postinstall.sh" "$BUILD_DIR/scripts/postinstall"
else
    echo "FATAL: $SCRIPT_DIR/postinstall.sh does not exist!"
    exit 1
fi
chmod +x "$BUILD_DIR/scripts/postinstall"

if [ -f "$SCRIPT_DIR/uninstall.sh" ]; then
    cp "$SCRIPT_DIR/uninstall.sh" "$RESOURCES_DIR/uninstall"
    chmod +x "$RESOURCES_DIR/uninstall"
else
    echo "No such file: $SCRIPT_DIR/uninstall.sh"
fi

echo "Building package..."
mkdir -p "$BUILD_DIR/payload/Applications/SSoTme"
cp -r "$RESOURCES_DIR"/* "$BUILD_DIR/payload/Applications/SSoTme/"

echo "Verifying code signature on copied binaries..."
codesign -dv --verbose=4 "$RESOURCES_DIR/ssotme"
codesign -dv --verbose=4 "$RESOURCES_DIR/aic"
codesign -dv --verbose=4 "$RESOURCES_DIR/aicapture"

# Build a single package directly
pkgbuild --root "$BUILD_DIR/payload" \
    --install-location "/" \
    --scripts "$BUILD_DIR/scripts" \
    --identifier "com.effortlessapi.ssotmecli" \
    --version "$SSOTME_VERSION" \
    "$BIN_DIR/unsigned_$THE_INSTALLER_FILENAME"

echo "Signing package $BIN_DIR/unsigned_$THE_INSTALLER_FILENAME -> $BIN_DIR/$THE_INSTALLER_FILENAME"
productsign --sign $DEV_INSTALLER_KEYCHAIN_ID "$BIN_DIR/unsigned_$THE_INSTALLER_FILENAME"  "$BIN_DIR/$THE_INSTALLER_FILENAME"

echo "Build completed. Installer is at: $BIN_DIR/$THE_INSTALLER_FILENAME"

echo "Running notary tool..."
NOTARY_RESULT=$(xcrun notarytool submit "$BIN_DIR/$THE_INSTALLER_FILENAME" --apple-id $APPLE_EMAIL \
                                         --password $NOTARYPASS \
                                         --team-id SLMGMPYNKS --wait \
                                         --output-format json)

echo "$NOTARY_RESULT"

# Check if notarization was successful
if echo "$NOTARY_RESULT" | grep -q '"status":"Accepted"'; then
    echo "Stapling notarization ticket to package..."
    if xcrun stapler staple -v "$BIN_DIR/$THE_INSTALLER_FILENAME"; then
        echo "Stapling completed successfully!"
    else
        echo "Stapling failed, package might be blocked from being opened"
    fi
else
    echo "Notarization failed. Package will not pass Gatekeeper validation."
    echo "See the notarization result for details."
fi
