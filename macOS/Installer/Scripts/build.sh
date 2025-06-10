#!/bin/bash
# Build script for SSoTme macOS Installer
#
# Generates:
#           - cli/macOS/Installer/bin/SSoTme-Installer.pkg


THE_INSTALLER_FILENAME=$1
DEV_INSTALLER_KEYCHAIN_ID=$2
DEV_EXECUTABLE_KEYCHAIN_ID=$3
APPLE_EMAIL=$4
NOTARYPASS=$5
PYTHON_VENVPATH=$6

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
RELEASE_FOLDER="$ROOT_DIR/release"
SSOTME_DIR="$HOME/.ssotme"
SSOTME_VERSION=$(grep -o '"version": "[^"]*"' "$ROOT_DIR/package.json" | cut -d'"' -f4)


echo "Using version: $SSOTME_VERSION from package.json"

# Update the version in the .csproj file
CSPROJ_FILE="$ROOT_DIR/Windows/CLI/SSoTme.OST.CLI.csproj"
if [ -f "$CSPROJ_FILE" ]; then
    echo "Current package.json version: $SSOTME_VERSION"
    read -p "Enter new csproj version (leave blank to inherit): " NEW_CSPROJ_VERSION
    
    if [ -z "$NEW_CSPROJ_VERSION" ]; then
        NEW_CSPROJ_VERSION="$SSOTME_VERSION"
        echo "Using package.json version: $NEW_CSPROJ_VERSION"
    else
        echo "Using custom version: $NEW_CSPROJ_VERSION"
    fi
else
    echo "WARNING: $CSPROJ_FILE not found"
    read -p "Enter new csproj version: " NEW_CSPROJ_VERSION
fi

echo "Updating version in $CSPROJ_FILE to $NEW_CSPROJ_VERSION"
sed -i '' "s/<Version>[^<]*<\/Version>/<Version>$NEW_CSPROJ_VERSION<\/Version>/g" "$CSPROJ_FILE"

# Clean previous builds
sudo rm -rf "$DIST_DIR"
sudo rm -rf "$BUILD_DIR"
sudo rm -rf "$BIN_DIR"
sudo rm -rf "$ROOT_DIR/build"
sudo rm -f release/*.pkg

echo "Creating necessary directories..."
mkdir -p "$RESOURCES_DIR" "$BUILD_DIR" "$DIST_DIR" "$ASSETS_DIR" "$BIN_DIR" "$BIN_DIR/signed" "$BIN_DIR/unsigned"

# Copy README into Resources
README_SRC="$ROOT_DIR/README.md"
README_DEST="$RESOURCES_DIR/README.md"
if [ -f "$README_SRC" ]; then
    cp "$README_SRC" "$README_DEST"
else
    echo "WARNING: README.md not found at root."
fi

echo "Building cli.py..."
/bin/bash "$SOURCE_DIR/build-cli.sh" "$PYTHON_VENVPATH"

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

# we need to make sure that the target mac's cpu type matches the type we're building on
TARGET_ARCH=$(uname -m)
echo "#!/bin/bash
      TARGET_ARCH=\"$TARGET_ARCH\" 
      if [ \$(uname -m) != \"\$TARGET_ARCH\" ]; then 
        echo \"[ERROR] CPU mismatch: expected \$TARGET_ARCH, got \$(uname -m)\" >&2 
        exit 64  # error code other than 1 
      fi
" > "$BUILD_DIR/scripts/preinstall"
chmod +x "$BUILD_DIR/scripts/preinstall"

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
    "$BIN_DIR/unsigned/$THE_INSTALLER_FILENAME"

echo "Signing package $BIN_DIR/unsigned/$THE_INSTALLER_FILENAME -> $BIN_DIR/signed/$THE_INSTALLER_FILENAME"
productsign --sign $DEV_INSTALLER_KEYCHAIN_ID "$BIN_DIR/unsigned/$THE_INSTALLER_FILENAME"  "$BIN_DIR/signed/$THE_INSTALLER_FILENAME"

echo "Build completed. Installer is at: $BIN_DIR/signed/$THE_INSTALLER_FILENAME"

echo ""
echo "$SCRIPT_DIR/notarize.sh" "$BIN_DIR/signed/$THE_INSTALLER_FILENAME" $APPLE_EMAIL $NOTARYPASS
/bin/bash "$SCRIPT_DIR/notarize.sh" "$BIN_DIR/signed/$THE_INSTALLER_FILENAME" $APPLE_EMAIL $NOTARYPASS

cp "$BIN_DIR/signed/$THE_INSTALLER_FILENAME" "$RELEASE_FOLDER/$THE_INSTALLER_FILENAME"

# run on the x86 one too
echo "$SCRIPT_DIR/notarize.sh" "$RELEASE_FOLDER/SSoTme-Installer-x86_64.pkg" $APPLE_EMAIL $NOTARYPASS
/bin/bash "$SCRIPT_DIR/notarize.sh" "$RELEASE_FOLDER/SSoTme-Installer-x86_64.pkg" $APPLE_EMAIL $NOTARYPASS

open "$RELEASE_FOLDER" -a Finder
