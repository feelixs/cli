# Build script for SSoTme Windows Installer (WiX v6)
# Integrates with setup.py for Python CLI building via PyInstaller
#
# Generates:
#           - bin/cli-installer/Release/CLI_Installer.msi -> installs just ssotme
#           - bin/main/Release/SSoTmeInstaller.exe -> installs .NET & ssotme

param (
    [string]$Configuration = "Release",
    [string]$Platform = "x86"
)

$ErrorActionPreference = "Stop"

# Ensure we're running from the Scripts directory
$ScriptDir = $PSScriptRoot
if (-not $ScriptDir) {
    $ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
}

# Calculate paths relative to script location
$InstallerDir = Split-Path -Parent $ScriptDir
$RootDir = Split-Path -Parent (Split-Path -Parent $InstallerDir)

# Validate we're in the right place
if (-not (Test-Path (Join-Path $RootDir "setup.py"))) {
    Write-Error "Cannot find setup.py at expected location: $(Join-Path $RootDir "setup.py")"
    Write-Error "Please run this script from Windows/Installer/Scripts/ directory"
    exit 1
}

$SourceDir = Join-Path $RootDir "Windows\CLI"
$ResourcesDir = Join-Path $InstallerDir "Resources"
$AssetsDir = Join-Path $InstallerDir "Assets"
$binFolder = Join-Path $InstallerDir "bin"
$OutputDir = Join-Path $binFolder "cli-installer\$Configuration"
$distDir = Join-Path $RootDir "dist"
$ssotmeDir = Join-Path $HOME ".ssotme"
$releaseDir = Join-Path $RootDir "release"

Write-Host "=== SSoTme WiX v6 Build Script ===" -ForegroundColor Green
Write-Host "Configuration: $Configuration" -ForegroundColor Cyan
Write-Host "Platform: $Platform" -ForegroundColor Cyan
Write-Host "Script Directory: $ScriptDir" -ForegroundColor Cyan
Write-Host "Installer Directory: $InstallerDir" -ForegroundColor Cyan
Write-Host "Root Directory: $RootDir" -ForegroundColor Cyan

# clean any previous builds
Write-Host "`nCleaning previous builds..." -ForegroundColor Yellow
Remove-Item -Recurse -Force -ErrorAction SilentlyContinue $distDir
Remove-Item -Recurse -Force -ErrorAction SilentlyContinue $ResourcesDir  # resources are copied into here from the root dir during build
Remove-Item -Recurse -Force -ErrorAction SilentlyContinue (Join-Path $RootDir "build")
Remove-Item -Recurse -Force -ErrorAction SilentlyContinue $binFolder
Remove-Item -Recurse -Force -ErrorAction SilentlyContinue (Join-Path $InstallerDir "obj")

Write-Host "Creating necessary directories..." -ForegroundColor Yellow
$Directories = @(
    $ResourcesDir,
    $OutputDir,
    $distDir,
    $AssetsDir,
    $releaseDir
)

Remove-Item -Path "$releaseDir\*.exe" -Force -ErrorAction SilentlyContinue

foreach ($Dir in $Directories) {
    if (-not (Test-Path $Dir)) {
        New-Item -ItemType Directory -Path $Dir -Force | Out-Null
        Write-Host "Created directory: $Dir" -ForegroundColor Green
    }
}

# copy LICENSE from the root dir into Resources/ (this will be included in the app, while Assets/LICENSE.rtf is only showed at install time)
Write-Host "`nCopying license and documentation files..." -ForegroundColor Yellow
$licenseSrc = Join-Path $RootDir "LICENSE"
$licenseDest = Join-Path $ResourcesDir "LICENSE.txt"
if (Test-Path $licenseSrc) {
    Copy-Item $licenseSrc -Destination $licenseDest -Force
    Write-Host "Copied LICENSE to Resources/" -ForegroundColor Green
}
else {
    Write-Warning "LICENSE file not found at root - installer will continue without it"
}

# copy README into Resources/
$rdmeSrc = Join-Path $RootDir "README.md"
$rdmeDest = Join-Path $ResourcesDir "README.md"
if (Test-Path $rdmeSrc) {
    Copy-Item $rdmeSrc -Destination $rdmeDest -Force
    Write-Host "Copied README.md to Resources/" -ForegroundColor Green
}
else {
    Write-Warning "README.md not found at root - installer will continue without it"
}

Write-Host "`nBuilding .NET CLI project..." -ForegroundColor Yellow
Push-Location $distDir
try {
    Set-Location $SourceDir
    dotnet build -c Release
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Failed to build .NET CLI project"
        exit 1
    }
    Write-Host ".NET project built successfully" -ForegroundColor Green
}
finally {
    Pop-Location
}

Write-Host "Building Python CLI with PyInstaller via setup.py..."

$SetupPath = Join-Path $RootDir "setup.py"

# Check if setup.py exists
if (-not (Test-Path $SetupPath)) {
    Write-Error "setup.py not found at $SetupPath"
    exit 1
}

# Check if Python is available
$pythonPath = (Get-Command python.exe -ErrorAction SilentlyContinue).Source
if (-not $pythonPath) {
    $pythonPath = (Get-Command python -ErrorAction SilentlyContinue).Source
    if (-not $pythonPath) {
        Write-Error "Python not found. Please install Python and ensure it's in your PATH."
        exit 1
    }
}
Write-Host "Using Python at: $pythonPath"

# Install requirements if requirements.txt exists
$requirementsPath = Join-Path $RootDir "requirements.txt"
if (Test-Path $requirementsPath) {
    Write-Host "Installing Python requirements..."
    try {
        & $pythonPath -m pip install -r $requirementsPath
        if ($LASTEXITCODE -ne 0) {
            Write-Warning "Failed to install some requirements, continuing anyway..."
        }
    }
    catch {
        Write-Warning "Error installing requirements: $_"
    }
}

# Substitute setuptools with pyinstaller_setuptools for PyInstaller build
$originalContent = Get-Content $SetupPath -Raw
$modifiedContent = $originalContent -replace 'from setuptools import setup', 'from pyinstaller_setuptools import setup'
$modifiedContent | Set-Content $SetupPath -NoNewline
Write-Host "Modified setup.py to use pyinstaller_setuptools"

# Run PyInstaller build via setup.py
Push-Location $RootDir
try {
    Write-Host "Running PyInstaller build..."
    $iconPath = Join-Path $AssetsDir "Icon.ico"
    if (-not (Test-Path $iconPath)) {
        Write-Warning "Icon file not found at $iconPath, building without icon"
        & $pythonPath .\setup.py pyinstaller -- -n ssotme --console --onefile --add-data "ssotme:ssotme" --hidden-import json
    }
    else {
        & $pythonPath .\setup.py pyinstaller -- -n ssotme --console --onefile --add-data "ssotme:ssotme" --hidden-import json --icon $iconPath
    }
    
    if ($LASTEXITCODE -ne 0) {
        Write-Error "PyInstaller build failed"
        # Revert setup.py before exiting
        $originalContent | Set-Content $SetupPath -NoNewline
        exit 1
    }
}
finally {
    Pop-Location
}

# Revert setup.py changes
$originalContent | Set-Content $SetupPath -NoNewline
Write-Host "Reverted setup.py to original state"

Write-Host "PyInstaller build completed successfully."

Push-Location $distDir
try {
    # Verify that PyInstaller generated the executable
    $ssotmeExePath = Join-Path $distDir "ssotme.exe"
    if (-not (Test-Path $ssotmeExePath)) {
        Write-Error "ssotme.exe not found in dist directory: $distDir"
        Write-Host "Contents of dist directory:"
        Get-ChildItem $distDir -ErrorAction SilentlyContinue | ForEach-Object { Write-Host "  $_" }
        exit 1
    }

    Write-Host "Found ssotme.exe, creating alias executables..."
    
    # Copy the ssotme.exe into ssotme, aic, aicapture (these are the CLI entry points)
    Copy-Item -Path $ssotmeExePath -Destination "$ResourcesDir/ssotme.exe" -Force
    Copy-Item -Path $ssotmeExePath -Destination "$ResourcesDir/aic.exe" -Force
    Copy-Item -Path $ssotmeExePath -Destination "$ResourcesDir/aicapture.exe" -Force

    Write-Host "Created alias executables in: $ResourcesDir"
    
    # Copy the config json from the home dir (this is generated by setup.py when pyinstaller runs)
    $configSourcePath = Join-Path $ssotmeDir "dotnet_info.json"
    $configDestPath = Join-Path $ResourcesDir "dotnet_info.json"
    
    if (Test-Path $configSourcePath) {
        Copy-Item -Path $configSourcePath -Destination $configDestPath -Force
        Write-Host "Copied dotnet_info.json from $configSourcePath to $configDestPath"
    }
    else {
        Write-Warning "dotnet_info.json not found at $configSourcePath"
        Write-Host "Creating placeholder dotnet_info.json..."
        
        # Create a basic placeholder config
        $placeholderConfig = @{
            "installed_versions" = @()
            "using_version"      = "7.0.410"
            "executable_path"    = "dotnet"
            "ssotme_version"     = $ssotmeVersion
        } | ConvertTo-Json -Depth 3
        
        $placeholderConfig | Set-Content $configDestPath -Encoding UTF8
        Write-Host "Created placeholder config at $configDestPath"
    }
}
finally {
    Pop-Location
}

Set-Location $InstallerDir

$packageJsonTxt = Get-Content (Join-Path $RootDir "package.json") -Raw | ConvertFrom-Json
$ssotmeVersion = $packageJsonTxt.version -replace "0", ""  #  Invalid product version '2024.08.23'. Product version must have a major version less than 256, a minor version less than 256
Write-Host "Using version: $ssotmeVersion from package.json"

# Update versions in all files if needed
$installerProj = Join-Path $InstallerDir "SSoTmeInstaller.wixproj"
$bootstrapperProj = Join-Path $InstallerDir "SSoTmeBootstrapper.wixproj"
$bootstrapperWxs = Join-Path $InstallerDir "Bootstrapper.wxs"

# Helper function to update version if changed
function Update-VersionIfChanged {
    param (
        [string]$FilePath,
        [string]$NewVersion,
        [string]$Pattern = '<SSoTmeVersion>([^<]*)</SSoTmeVersion>',
        [string]$Replacement = "<SSoTmeVersion>$NewVersion</SSoTmeVersion>"
    )
    $content = Get-Content $FilePath -Raw
    if ($content -match $Pattern) {
        $currentVersion = $matches[1]
        if ($currentVersion -ne $NewVersion) {
            $content = $content -replace $Pattern, $Replacement
            Set-Content $FilePath $content -NoNewline
            Write-Host "Updated $($(Split-Path $FilePath -Leaf)) from $currentVersion to $NewVersion" -ForegroundColor Green
            return $true
        } else {
            Write-Host "Version in $($(Split-Path $FilePath -Leaf)) already at $NewVersion" -ForegroundColor Cyan
            return $false
        }
    }
    return $false
}

# Update versions in all files
Update-VersionIfChanged -FilePath $installerProj -NewVersion $ssotmeVersion
Update-VersionIfChanged -FilePath $bootstrapperProj -NewVersion $ssotmeVersion
Update-VersionIfChanged -FilePath $bootstrapperWxs -NewVersion $ssotmeVersion `
    -Pattern '<Bundle([^>]*?)Version="([^"]*)"([^>]*?)>' `
    -Replacement "<Bundle`$1Version=`"$ssotmeVersion`"`$3>"

# Build the WiX projects using dotnet build
Write-Host "Building WiX installer projects..."
try {
    # Check if .NET SDK is available (WiX v6 requires .NET)
    $dotnetPath = (Get-Command dotnet.exe -ErrorAction SilentlyContinue).Source

    if (-not $dotnetPath) {
        Write-Error ".NET SDK not found. WiX v6 requires .NET SDK. Please install from https://dotnet.microsoft.com/download"
        return
    }

    Write-Host "Building MSI package..."
    
    # Ensure we're in the installer directory for the build
    Set-Location $InstallerDir
    
    # Build the MSI project first (this should only compile Product.wxs)
    Write-Host "Building SSoTmeInstaller.wixproj (MSI)..." -ForegroundColor Cyan
    & dotnet build SSoTmeInstaller.wixproj --configuration $Configuration --verbosity normal
    
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Failed to build MSI project"
        return
    }

    $MsiPath = Join-Path $OutputDir "CLI_Installer.msi"
    if (-Not (Test-Path $MsiPath)) {
        Write-Host "ERROR: CLI_Installer.msi not found at expected location: $MsiPath"
        Write-Host "Contents of output directory:" -ForegroundColor Yellow
        Get-ChildItem $OutputDir -ErrorAction SilentlyContinue | ForEach-Object { Write-Host "  $_" -ForegroundColor Yellow }
        exit 1
    }
    Write-Host "MSI built successfully: $MsiPath" -ForegroundColor Green

    Write-Host "Building Bundle (Bootstrapper)..."
    # Build the Bundle project (this should only compile Bootstrapper.wxs)
    Write-Host "Building SSoTmeBootstrapper.wixproj (Bundle)..." -ForegroundColor Cyan
    & dotnet build SSoTmeBootstrapper.wixproj --configuration $Configuration --verbosity normal

    if ($LASTEXITCODE -ne 0) {
        Write-Error "Failed to build Bundle project"
        return
    }

    Write-Host "WiX installer built successfully"

    # Output the paths to the created installers
    $BundleExePath = Join-Path (Join-Path $binFolder "main\$Configuration") "SSoTmeInstaller.exe"
    
    if (Test-Path $MsiPath) {
        Write-Host "MSI installer created at: $MsiPath"
    }
    else {
        Write-Error "MSI installer not created. Check the build logs for errors."
    }
    
    if (Test-Path $BundleExePath) {
        Write-Host "Bundle installer created at: $BundleExePath"
    }
    else {
        Write-Error "Bundle installer not created. Check the build logs for errors."
    }
}
catch {
    Write-Error "Failed to build WiX installer: $_"
}
finally {
    # Always return to the original directory
    Set-Location $ScriptDir
}

Write-Host "Build script completed" -ForegroundColor Green