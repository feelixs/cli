# SSoTme CLI macOS Installer

This directory contains scripts and resources for building a macOS installer package for the SSoTme CLI.

## Directory Structure

- `/Assets` - Contains assets used by the installer (icons, license RTF)
- `/Resources` - Temporary directory where resources are copied during build
- `/Scripts` - Contains the build scripts for the installer

## Building the Installer

To build the installer:

1. Ensure you have the macOS Command Line Tools installed:
   ```
   xcode-select --install
   ```

2. Run the build script:
   ```
   cd /path/to/cli/Windows/macOS_Installer/Scripts
   ./build.sh
   ```

3. The installer package will be created at:
   ```
   /path/to/cli/dist/SSoTme-Installer.pkg
   ```

## Installer Package Details

The installer will:

1. Install the SSoTme CLI to `/Applications/SSoTme/`
2. Create symbolic links in `/usr/local/bin/` for the `ssotme`, `aic`, and `aicapture` commands
3. Set up the required configuration in the user's home directory

## Requirements

- macOS 10.12 or later
- Administrative privileges are required for installation

## Customization

- Update `Assets/Icon.ico` with a proper macOS icon file (.icns)
- Modify `Assets/LICENSE.rtf` with your license agreement
- To customize the installer appearance, modify the relevant sections in the `build.sh` script