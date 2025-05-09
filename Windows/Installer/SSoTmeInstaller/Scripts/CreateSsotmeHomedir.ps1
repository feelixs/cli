$source = "$PSScriptRoot\..\Resources\dotnet_info.json"
$destinationFolder = "$HOME\.ssotme"
$destinationFile = Join-Path $destinationFolder "dotnet_info.json"

if (-not (Test-Path $destinationFolder)) {
    New-Item -Path $destinationFolder -ItemType Directory -Force
}

Copy-Item -Path $source -Destination $destinationFile -Force
Write-Host "dotnet_info.json has been copied to $destinationFile"
