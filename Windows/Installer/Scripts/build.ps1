# Build script for SSoTme Windows Installer
#
# Generates:
#           - bin/cli-installer/Release/CLI-Installer.msi -> installs just ssotme
#           - bin/main/Release/SSoTmeInstaller.exe -> installs .NET & ssotme


param (
    [string]$Configuration = "Release",
    [string]$Platform = "x64"
)

$ErrorActionPreference = "Stop"
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$InstallerDir = Split-Path -Parent $ScriptDir
$RootDir = Split-Path -Parent (Split-Path -Parent $InstallerDir)
$SourceDir = Join-Path $RootDir "Windows\CLI"
$ResourcesDir = Join-Path $InstallerDir "Resources"
$AssetsDir = Join-Path $InstallerDir "Assets"
$binFolder = Join-Path $InstallerDir "bin"
$OutputDir = Join-Path $binFolder "cli-installer\$Configuration"
$distDir = Join-Path $RootDir "dist"
$ssotmeDir = Join-Path $HOME ".ssotme"

# clean any previous builds
Remove-Item -Recurse -Force -ErrorAction SilentlyContinue $distDir
Remove-Item -Recurse -Force -ErrorAction SilentlyContinue $ResourcesDir  # resources are copied into here from the root dir during build
Remove-Item -Recurse -Force -ErrorAction SilentlyContinue (Join-Path $RootDir "build")
Remove-Item -Recurse -Force -ErrorAction SilentlyContinue $binFolder
Remove-Item -Recurse -Force -ErrorAction SilentlyContinue (Join-Path $InstallerDir "obj")

Write-Host "Creating necessary directories..."
$Directories = @(
    $ResourcesDir,
    $OutputDir,
    $distDir,
    $AssetsDir
)

foreach ($Dir in $Directories) {
    if (-not (Test-Path $Dir)) {
        New-Item -ItemType Directory -Path $Dir -Force | Out-Null
        Write-Host "Created directory: $Dir"
    }
}

# copy LISENCE from the root dir into Resources/ (this will be included in the app, while Assets/LICENSE.rtf is only showed at install time)
$licenseSrc = Join-Path $RootDir "LICENSE"
$licenseDest = Join-Path $ResourcesDir "LICENSE.txt"
if (Test-Path $licenseSrc) {
    Copy-Item $licenseSrc -Destination $licenseDest -Force
} else {
    Write-Warning "LICENSE file not found at root."
}

# copy README into Resources/
$rdmeSrc = Join-Path $RootDir "README.md"
$rdmeDest = Join-Path $ResourcesDir "README.md"
if (Test-Path $rdmeSrc) {
    Copy-Item $rdmeSrc -Destination $rdmeDest -Force
} else {
    Write-Warning "README.md not found at root."
}

Write-Host "Building .NET CLI project..."
Push-Location $distDir
try {
    Set-Location $SourceDir
    dotnet build -c Release
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Failed to build .NET CLI project"
        exit 1
    }
} finally {
    Pop-Location
}

Write-Host "Building cli.py..."
# PowerShell script to build the CLI with PyInstaller modifications

$ErrorActionPreference = "Stop"

$SetupPath = Join-Path $RootDir "setup.py"

# Substitute setuptools with pyinstaller_setuptools
$originalContent = Get-Content $SetupPath
$modifiedContent = $originalContent -replace 'setuptools', 'pyinstaller_setuptools'
$modifiedContent | Set-Content $SetupPath
Write-Host "Modified setup.py to use pyinstaller_setuptools"

# Run PyInstaller build via setup.py
Push-Location $ProjectDir
try {
    python .\setup.py pyinstaller -- -n ssotme --console --onefile --add-data "ssotme:ssotme" --hidden-import json --icon (Join-Path $AssetsDir "Icon.ico")
} finally {
    Pop-Location
}

# Revert setup.py changes
$originalContent | Set-Content $SetupPath
Write-Host "Reverted setup.py to original state"

Write-Host "Build completed."

Push-Location $distDir
try {
    # copy the files generated during pyinstaller build of setup.py
    # copy the ssotme.exe into ssotme, aic, aicapture
    Copy-Item -Path "ssotme.exe" -Destination "$ResourcesDir/ssotme.exe" -Force
    Copy-Item -Path "ssotme.exe" -Destination "$ResourcesDir/aic.exe" -Force
    Copy-Item -Path "ssotme.exe" -Destination "$ResourcesDir/aicapture.exe" -Force

    # copy the config json from the home dir
    Copy-Item -Path (Join-Path $ssotmeDir "dotnet_info.json") -Destination (Join-Path $ResourcesDir "dotnet_info.json") -Force

    Write-Host "Created alias executables in: $ResourcesDir"
} finally {
    Pop-Location
}

Set-Location $InstallerDir

$packageJsonTxt = Get-Content (Join-Path $RootDir "package.json") -Raw | ConvertFrom-Json
$ssotmeVersion = $packageJsonTxt.version -replace "0", ""  #  Invalid product version '2024.08.23'. Product version must have a major version less than 256, a minor version less than 256
Write-Host "Using version: $ssotmeVersion from package.json"

$projConfig = Join-Path $InstallerDir "Bootstrapper.wxs"
$projConfigTxt = Get-Content $projConfig
$newConfigTxt = $projConfigTxt -replace '(<Bundle\s+Name="[^"]*"\s+Version=")[^"]*(")', "`${1}$ssotmeVersion`${2}"

Set-Content $projConfig $newConfigTxt
Write-Host "Updated Bootstrapper.wxs with version $ssotmeVersion"

$projConfig = Join-Path $InstallerDir "SSoTmeInstaller.wixproj"
$projConfigTxt = Get-Content $projConfig -Raw
$newConfigTxt = $projConfigTxt -replace '<SSoTmeVersion>.*?</SSoTmeVersion>', "`<SSoTmeVersion>$ssotmeVersion`</SSoTmeVersion>"

Set-Content $projConfig $newConfigTxt -Encoding UTF8
Write-Host "Updated SSoTmeInstaller.wixproj with version $ssotmeVersion"


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
    Push-Location $InstallerDir
    
    # Build the WiX project
    Write-Host "msbuild .\SSoTmeInstaller.wixproj /p:Configuration=$Configuration /p:Platform=$Platform"
    & msbuild .\SSoTmeInstaller.wixproj /p:Configuration=$Configuration /p:Platform=$Platform

    $MsiPath = Join-Path $OutputDir "CLI-Installer.msi"
    if (-Not (Test-Path $MsiPath)) {
        Write-Host "ERROR: CLI-Installer.msi not found at expected location: $MsiPath"
        exit 1
    }

    Write-Host "msbuild .\SSoTmeBootstrapper.wixproj /p:Configuration=$Configuration /p:Platform=$Platform"
    & msbuild .\SSoTmeBootstrapper.wixproj /p:Configuration=$Configuration /p:Platform=$Platform

    if ($LASTEXITCODE -ne 0) {
        Write-Error "Failed to build WiX project"
        return
    }
    
    Write-Host "WiX installer built successfully"
    
    # Output the path to the MSI
    $FinalExePath = Join-Path (Join-Path $binFolder "main") "SSoTmeInstaller.exe"
    if (Test-Path $MsiPath) {
        Write-Host "Installer created at: $MsiPath"
    }
    else {
        Write-Error "Installer not created. Check the WiX build logs for errors. Checked path: $FinalExePath"
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
