#!/bin/bash
# Build script for SSoTme macOS Installer
#
# Generates:
#           - dist/SSoTme-Installer.pkg - macOS installer package

INSTALLER_DIR="$( dirname "${BASH_SOURCE[0]}" )"

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
SSOTME_DIR="$HOME/.ssotme"
SSOTME_VERSION=$(grep -o '"version": "[^"]*"' "$ROOT_DIR/package.json" | cut -d'"' -f4)


echo "Using version: $SSOTME_VERSION from package.json"

# Clean previous builds
rm -rf "$DIST_DIR"
rm -rf "$BUILD_DIR"
rm -rf "$ROOT_DIR/build"

echo "Creating necessary directories..."
mkdir -p "$RESOURCES_DIR" "$BUILD_DIR" "$DIST_DIR" "$ASSETS_DIR"

# Copy LICENSE and README into Resources
LICENSE_SRC="$ROOT_DIR/LICENSE"
LICENSE_DEST="$RESOURCES_DIR/LICENSE.txt"
if [ -f "$LICENSE_SRC" ]; then
    cp "$LICENSE_SRC" "$LICENSE_DEST"
else
    echo "WARNING: LICENSE file not found at root."
fi

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

mkdir -p "$BUILD_DIR/scripts"
cat > "$BUILD_DIR/scripts/postinstall" << 'EOF'
#!/bin/bash
# Post-installation script

# Create ~/.ssotme directory
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

exit 0
EOF

chmod +x "$BUILD_DIR/scripts/postinstall"

# Create package
echo "Building package..."
mkdir -p "$BUILD_DIR/payload/Applications/SSoTme"
cp -r "$RESOURCES_DIR"/* "$BUILD_DIR/payload/Applications/SSoTme/"

# Build component package
pkgbuild --root "$BUILD_DIR/payload" \
    --install-location "/" \
    --scripts "$BUILD_DIR/scripts" \
    --identifier "com.ssotme.cli" \
    --version "$SSOTME_VERSION" \
    "$BUILD_DIR/SSoTme-CLI.pkg"


if [ -f "$ASSETS_DIR/distribution.xml" ]; then
    cp "$ASSETS_DIR/distribution.xml" "$BUILD_DIR/distribution.xml"
else
    echo "No such file: $ASSETS_DIR/distribution.xml"
fi

# Create required resource files for the installer UI
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
    "$INSTALLER_DIR/SSoTme-Installer.pkg"


if command -v fileicon >/dev/null 2>&1; then
  fileicon set "$INSTALLER_DIR/SSoTme-Installer.pkg" "$ASSETS_DIR/Icon.icns"
else
  echo "fileicon was not found, please run: brew install fileicon"
fi

echo "Build completed. Installer is at: $DIST_DIR/SSoTme-Installer.pkg"

# todo install dotnet in postinstall