# Build script for SSoTme CLI
$ErrorActionPreference = "Stop"

# Create output directory
$distDir = "dist"
if (-not (Test-Path -Path $distDir)) {
    New-Item -ItemType Directory -Path $distDir | Out-Null
    Write-Host "Created output directory: $distDir"
}

# Step 1: Build the .NET solution
Write-Host "Building .NET solution..."
dotnet build SSoTme-OST-CLI.sln --configuration Release
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to build .NET solution"
    exit 1
}

# Step 2: Build the C# CLI wrapper (if on Windows)
if ($IsWindows -or $env:OS -match "Windows") {
    Write-Host "Building Windows native CLI wrapper..."
    
    # Navigate to the CLI project directory
    Push-Location "Windows/Installer/CLI"
    try {
        # Build the C# CLI wrapper
        dotnet publish -c Release --self-contained -r win-x64 -p:PublishSingleFile=true
        if ($LASTEXITCODE -ne 0) {
            Write-Error "Failed to build C# CLI wrapper"
            exit 1
        }
        
        # Get the build output path
        $buildOutput = "bin/Release/net7.0/win-x64/publish"
        
        # Create the Windows directory structure in dist
        $targetNetPath = "../../..$distDir/Windows/CLI/bin/Release/net7.0"
        if (-not (Test-Path -Path $targetNetPath)) {
            New-Item -ItemType Directory -Path $targetNetPath -Force | Out-Null
        }
        
        # Copy the .NET DLLs to the dist folder
        Copy-Item -Path "../../../Windows/CLI/bin/Release/net7.0/*" -Destination $targetNetPath -Recurse -Force
        
        # Copy the executables to the dist folder
        Copy-Item -Path "$buildOutput/ssotme.exe" -Destination "../../../$distDir/" -Force
        Copy-Item -Path "$buildOutput/ssotme.exe" -Destination "../../../$distDir/aic.exe" -Force
        Copy-Item -Path "$buildOutput/ssotme.exe" -Destination "../../../$distDir/aicapture.exe" -Force
        
        Write-Host "Created Windows executables in: $distDir"
    } finally {
        Pop-Location
    }
} else {
    Write-Host "Skipping Windows native CLI wrapper build (not on Windows)"
}

Write-Host "Build completed successfully!"