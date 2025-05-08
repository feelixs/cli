# SSoTme Windows Installer

This directory contains the Windows Installer project for the SSoTme CLI tool.

## Project Structure

```
Windows/Installer/
├── SSoTmeInstaller/               # WiX project directory
│   ├── Product.wxs                # Main WiX product definition
│   ├── SSoTmeInstaller.wixproj    # WiX project file
│   ├── Components/                # Component definitions
│   │   ├── DotNetComponent.wxs    # .NET dependency component
│   │   └── CLIComponent.wxs       # SSoTme CLI component
│   ├── UI/                        # Custom UI definitions
│   │   ├── CustomDialog.wxs       # Custom installer dialogs
│   │   └── InstallUI.wxs          # Main installer UI
│   ├── Resources/                 # Resources for the installer
│   │   ├── License.rtf            # License file
│   │   ├── Banner.png             # Banner image
│   │   └── Icon.ico               # Application icon
│   └── Assets/                    # Additional assets
│       └── DotNetInstaller.exe    # .NET installer if needed
└── Scripts/                       # Build and utility scripts
    ├── build.ps1                  # PowerShell build script
    └── prepare-assets.ps1         # Script to prepare assets
```

## Prerequisites

To build this installer, you'll need:

1. [WiX Toolset](https://wixtoolset.org/releases/) (version 3.11.2 or higher)
2. [Visual Studio](https://visualstudio.microsoft.com/) with the WiX Toolset extension
3. .NET SDK
4. PowerShell

## Getting Started

1. Install the prerequisites
2. Run the `build.ps1` script to build the installer

## Build Process

The build process:

1. Prepares the assets (downloads .NET installer if needed)
2. Creates the WiX project files
3. Builds the installer using the WiX Toolset
4. Produces an MSI file in the `bin/Release` directory

## Installation Features

- Detects and installs .NET if not present
- Installs the SSoTme CLI tool
- Adds the application to PATH
- Creates shortcuts in the Start Menu
- Registers file associations
- Adds uninstallation support