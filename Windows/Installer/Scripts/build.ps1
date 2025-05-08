# Build script for SSoTme Windows Installer
param (
    [string]$Configuration = "Release",
    [switch]$SkipPyInstaller = $false,
    [string]$distDir = "../../../dist/out/"
)

$ErrorActionPreference = "Stop"
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$RootDir = Split-Path -Parent (Split-Path -Parent (Split-Path -Parent $ScriptDir))
$InstallerDir = Join-Path $RootDir "Windows\Installer"
$WixProjectDir = Join-Path $InstallerDir "SSoTmeInstaller"
$ResourcesDir = Join-Path $WixProjectDir "Resources"
$OutputDir = Join-Path $WixProjectDir "bin\$Configuration"

Write-Host "Creating necessary directories..."
$Directories = @(
    $ResourcesDir,
    $OutputDir,
    $distDir,
    (Join-Path $WixProjectDir "Assets"),
    (Join-Path $WixProjectDir "Scripts")
)

foreach ($Dir in $Directories) {
    if (-not (Test-Path $Dir)) {
        New-Item -ItemType Directory -Path $Dir -Force | Out-Null
        Write-Host "Created directory: $Dir"
    }
}

# Copy LICENSE file
$licenseSrc = Join-Path $RootDir "LICENSE"
$licenseDest = Join-Path $distDir "LICENSE"
if (Test-Path $licenseSrc) {
    Copy-Item $licenseSrc -Destination $licenseDest -Force
    Write-Host "Copied LICENSE file to distribution directory."
} else {
    Write-Warning "LICENSE file not found at root."
}

# Create icon if it doesn't exist
$IconFile = Join-Path $ResourcesDir "Icon.ico"
if (-not (Test-Path $IconFile)) {
    Write-Host "Please create an icon file at: $IconFile"
}

Write-Host "Building cli.py..."
Push-Location $distDir
try {
    ./ssotme/build.sh
    Copy-Item Join-Path $RootDir "dist" $OutputDir
} finally {
    Pop-Location
}

Write-Host "Building .NET CLI project..."
Push-Location $distDir
try {
    dotnet build -c Release
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Failed to build .NET CLI project"
        exit 1
    }
} finally {
    Pop-Location
}

Push-Location $distDir
try {
    # Copy the main executable to the dist folder
    $mainExe = "ssotme.exe"
    Copy-Item -Path $mainExe -Destination "$distDir/" -Force

    # Create alias executables (copy the main executable)
    Copy-Item -Path $mainExe -Destination "$distDir/aic.exe" -Force
    Copy-Item -Path $mainExe -Destination "$distDir/aicapture.exe" -Force

    Write-Host "Created alias executables in: $distDir"
} finally {
    Pop-Location
}

# Copy the required .NET DLLs
$netDllPath = "../CLI/bin/Release/net7.0"
$targetNetPath = "$distDir/Windows/CLI/bin/Release/net7.0"

# Create the target directory structure
if (-not (Test-Path -Path $targetNetPath)) {
    New-Item -ItemType Directory -Path $targetNetPath -Force | Out-Null
}

# Copy all DLLs and config files
Copy-Item -Path "$netDllPath/*" -Destination $targetNetPath -Recurse -Force

Write-Host "Copied .NET DLLs to: $targetNetPath"
Write-Host "Build complete! Executables are in: $distDir"

# Build the WiX project
Write-Host "Building WiX installer project..."
try {
    # Check if WiX toolset is installed
    $WixPath = (Get-Command candle.exe -ErrorAction SilentlyContinue).Source
    
    if (-not $WixPath) {
        Write-Error "WiX Toolset not found. Please install WiX Toolset from https://wixtoolset.org/releases/"
        return
    }
    
    # Change directory to the WiX project
    Push-Location $WixProjectDir
    
    # Build the WiX project
    & msbuild /p:Configuration=$Configuration /p:Platform=x86
    
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Failed to build WiX project"
        return
    }
    
    Write-Host "WiX installer built successfully"
    
    # Output the path to the MSI
    $MsiPath = Join-Path $OutputDir "SSoTmeInstaller.msi"
    if (Test-Path $MsiPath) {
        Write-Host "Installer created at: $MsiPath"
    }
    else {
        Write-Error "Installer not created. Check the WiX build logs for errors."
    }
}
catch {
    Write-Error "Failed to build WiX installer: $_"
}
finally {
    # Return to the original directory
    Pop-Location
}

Write-Host "Build script completed"