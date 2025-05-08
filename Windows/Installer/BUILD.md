# Building the SSoTme Windows Installer

This document explains how to build the SSoTme Windows Installer package.

## Prerequisites

Before you begin, make sure you have the following tools installed:

1. **WiX Toolset** (v3.11.2 or higher)
   - Download from [WiX Toolset](https://wixtoolset.org/releases/)
   - Add the WiX Toolset bin directory to your PATH

2. **Visual Studio** with the following components:
   - .NET Desktop development workload
   - WiX Toolset Visual Studio Extension

3. **.NET SDK** (v7.0 or higher)
   - Required for building the .NET components

4. **Python** (v3.7 or higher)
   - With PyInstaller package installed: `pip install pyinstaller pyinstaller-setuptools`

5. **PowerShell** (v5.1 or higher)
   - Windows 10/11 includes this by default

## Building the Installer

### Step 1: Build the Python Executable

The installer requires the SSoTme CLI executable to be built first. This is handled automatically by the build script.

### Step 2: Run the Build Script

1. Open PowerShell
2. Navigate to the `Windows/Installer/Scripts` directory
3. Run the build script:

```powershell
.\build.ps1
```

This will:
- Build the Python executable (unless you use the `-SkipPyInstaller` switch)
- Create the necessary directories and resources
- Build the WiX installer project
- Generate the MSI file in `Windows/Installer/SSoTmeInstaller/bin/Release`

### Build Script Parameters

The build script supports the following parameters:

- `-Configuration`: Specifies the build configuration (Debug or Release, default is Release)
- `-SkipPyInstaller`: Skips the Python executable build step (use this if you've already built it)

Example:
```powershell
.\build.ps1 -Configuration Debug -SkipPyInstaller
```

## Customizing the Installer

### Customizing the UI

To customize the installer UI:

1. Edit the UI XML files in `Windows/Installer/SSoTmeInstaller/UI/`
2. Update the banner and icon files in `Windows/Installer/SSoTmeInstaller/Resources/`

### Customizing the Installation

To modify the installation behavior:

1. Edit the components in `Windows/Installer/SSoTmeInstaller/Components/`
2. Edit the product definition in `Windows/Installer/SSoTmeInstaller/Product.wxs`

### Changing the .NET Version

To change the .NET version requirement:

1. Update the version in `Windows/Installer/SSoTmeInstaller/Components/DotNetComponent.wxs`
2. Update the download URL in `Windows/Installer/SSoTmeInstaller/Scripts/DownloadDotNet.ps1`

## Troubleshooting

### Common Issues

1. **WiX Toolset not found**
   - Ensure WiX Toolset is installed and the bin directory is in your PATH
   - Try running the build script with administrator privileges

2. **Python executable build fails**
   - Check that PyInstaller and pyinstaller-setuptools are installed
   - Check console output for specific errors

3. **MSI build fails**
   - Look for detailed errors in the MSI build output
   - Ensure all necessary resources exist (banner, icon, license file)

### Getting Help

If you encounter issues that you can't resolve:
- Check the WiX Toolset documentation at https://wixtoolset.org/documentation/
- Review PyInstaller documentation at https://pyinstaller.org/en/stable/
- Open an issue on the project's GitHub repository

## Testing the Installer

Before distributing the installer, make sure to test it thoroughly:

1. Test on a clean VM to ensure all dependencies are properly handled
2. Verify that .NET is installed correctly if not present
3. Confirm that the CLI works after installation
4. Test the uninstallation process
5. Verify all shortcuts and registry entries

## Distribution

The final MSI file can be distributed to users and should handle all necessary installation steps, including .NET dependency management.