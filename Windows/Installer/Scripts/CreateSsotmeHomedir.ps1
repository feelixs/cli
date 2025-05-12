param (
    [string]$installPath
)

$cleanedInstallPath = Join-Path (Split-Path -Parent $installPath) "SSoTme"

$binPath = Join-Path $cleanedInstallPath "bin"
$SourceFilePath = Join-Path $binPath "dotnet_info.json"

Write-Host "Running copy home script with source file: $SourceFilePath"

# Create destination directory
$destinationFolder = "$HOME\.ssotme"
$destinationFile = Join-Path $destinationFolder "dotnet_info.json"

if (-not (Test-Path $destinationFolder)) {
    New-Item -Path $destinationFolder -ItemType Directory -Force
    Write-Host "Created directory: $destinationFolder"
}

# Verify source file exists
if (-not (Test-Path $SourceFilePath)) {
    Write-Host "ERROR: Source file not found at $SourceFilePath"
    exit 1
}

# Copy the file
Copy-Item -Path $SourceFilePath -Destination $destinationFile -Force
Write-Host "dotnet_info.json has been copied to $destinationFile"

# Delete the original file
try {
    Remove-Item -Path $SourceFilePath -Force
    Write-Host "Original dotnet_info.json deleted from $SourceFilePath"
}
catch {
    Write-Warning "Failed to delete original file: $_"
    exit 1
}
