#!/bin/bash
# Build script for SSoTme macOS Installer
#
# Generates:
#           - dist/SSoTme-Installer.pkg - macOS installer package


THE_INSTALLER_FILENAME=$1
DEV_KEYCHAIN_ID=$2

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
rm -rf "$DIST_DIR"
rm -rf "$BUILD_DIR"
rm -rf "$BIN_DIR"
rm -rf "$ROOT_DIR/build"

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

# Create package -> this will create the 'component package' inside the build/ folder
# the build/ folder will be passed into productbuild command later which will re-package everything for distribution
echo "Building package..."
mkdir -p "$BUILD_DIR/payload/Applications/SSoTme"
cp -r "$RESOURCES_DIR"/* "$BUILD_DIR/payload/Applications/SSoTme/"
pkgbuild --root "$BUILD_DIR/payload" \
    --install-location "/" \
    --scripts "$BUILD_DIR/scripts" \
    --identifier "com.effortlessapi.ssotmecli" \
    --version "$SSOTME_VERSION" \
    "$BUILD_DIR/tmp-SSoTme-CLI.pkg"

echo "Signing nested pkg tmp-SSoTme-CLI.pkg -> SSoTme-CLI.pkg"
sudo /bin/bash "$SCRIPT_DIR/packagesign.sh" "$BUILD_DIR/tmp-SSoTme-CLI.pkg" "$BUILD_DIR/SSoTme-CLI.pkg" $DEV_KEYCHAIN_ID
echo "Removing unsigned tmp-SSoTme-CLI.pkg"
sudo rm "$BUILD_DIR/tmp-SSoTme-CLI.pkg"


if [ -f "$ASSETS_DIR/distribution.xml" ]; then
    cp "$ASSETS_DIR/distribution.xml" "$BUILD_DIR/distribution.xml"
else
    echo "No such file: $ASSETS_DIR/distribution.xml"
fi

# License
if [ -f "$ASSETS_DIR/LICENSE.rtf" ]; then
    cp "$ASSETS_DIR/LICENSE.rtf" "$BUILD_DIR/license.rtf"
else
    echo "No such file: $ASSETS_DIR/LICENSE.rtf"
fi

# Build the final installer
productbuild --distribution "$BUILD_DIR/distribution.xml" \
    --resources "$BUILD_DIR" \
    --package-path "$BUILD_DIR" \
    "$BIN_DIR/unsigned_$THE_INSTALLER_FILENAME"

echo "Signing final package file $BIN_DIR/unsigned_$THE_INSTALLER_FILENAME -> $BIN_DIR/$THE_INSTALLER_FILENAME"
sudo /bin/bash "$SCRIPT_DIR/packagesign.sh" "$BIN_DIR/unsigned_$THE_INSTALLER_FILENAME" "$BIN_DIR/$THE_INSTALLER_FILENAME" $DEV_KEYCHAIN_ID

# set the icon of the product .pkg file
if command -v fileicon >/dev/null 2>&1; then
  fileicon set "$BIN_DIR/$THE_INSTALLER_FILENAME" "$ASSETS_DIR/Icon.icns"
else
  echo "fileicon was not found, please run: brew install fileicon"
fi

echo "Build completed. Installer is at: $BIN_DIR/$THE_INSTALLER_FILENAME"
