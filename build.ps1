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

# Step 2: Build the Python executables using PyInstaller
Write-Host "Building standalone executables with PyInstaller..."

if ($IsWindows -or $env:OS -match "Windows") {
    # For Windows, use build.sh through WSL or directly use python with pyinstaller
    try {
        Write-Host "Building Windows executables..."
        
        # Ensure PyInstaller is installed
        python -m pip install pyinstaller
        if ($LASTEXITCODE -ne 0) {
            Write-Error "Failed to install PyInstaller"
            exit 1
        }
        
        # Create the ssotme executable
        python -m PyInstaller --onefile --name ssotme --console ssotme/cli.py
        if ($LASTEXITCODE -ne 0) {
            Write-Error "Failed to build ssotme executable"
            exit 1
        }
        
        # Create aic and aicapture executables (copy the ssotme executable)
        Copy-Item -Path "dist/ssotme.exe" -Destination "dist/aic.exe" -Force
        Copy-Item -Path "dist/ssotme.exe" -Destination "dist/aicapture.exe" -Force

        Write-Host "Successfully built Windows executables."
    } catch {
        Write-Error "Failed to build Windows executables: $_"
        exit 1
    }
} else {
    # For macOS/Linux, run build.sh directly
    Write-Host "Running build.sh to create executables..."
    
    if (Test-Path -Path "ssotme/build.sh") {
        # Ensure the script is executable
        chmod +x ssotme/build.sh
        
        # Run the build script
        ./ssotme/build.sh
        if ($LASTEXITCODE -ne 0) {
            Write-Error "Failed to run build.sh"
            exit 1
        }
    } else {
        Write-Host "Building with PyInstaller directly..."
        # Fallback to direct PyInstaller commands
        python -m pip install pyinstaller
        python -m PyInstaller --onefile --name ssotme --console ssotme/cli.py
        
        # Create symbolic links for aic and aicapture
        if (Test-Path -Path "dist/ssotme") {
            ln -sf ssotme "dist/aic"
            ln -sf ssotme "dist/aicapture"
        }
    }
}

# Step 3: Copy the .NET DLLs to the dist folder
Write-Host "Copying .NET DLLs to dist folder..."

# Create the Windows directory structure in dist
$targetNetPath = "$distDir/Windows/CLI/bin/Release/net7.0"
if (-not (Test-Path -Path $targetNetPath)) {
    New-Item -ItemType Directory -Path $targetNetPath -Force | Out-Null
}

# Copy the .NET DLLs
Copy-Item -Path "Windows/CLI/bin/Release/net7.0/*" -Destination $targetNetPath -Recurse -Force

Write-Host "Build completed successfully! Executables are in the dist/ directory."