# DownloadDotNet.ps1
# Script to download the .NET SDK installer

$Architechture = "x86"
$dotnetVersion = "7.0.410"
$destinationPath = [System.IO.Path]::Combine($env:TEMP, "dotnet-sdk-$dotnetVersion-win-$Architechture.exe")
$downloadUrl = "https://builds.dotnet.microsoft.com/dotnet/Sdk/$dotnetVersion/dotnet-sdk-$dotnetVersion-win-$Architechture.exe"

# Ensure TLS 1.2 is used
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

try {
    Write-Host "Downloading .NET SDK $dotnetVersion..."

    # Create a web client
    $webClient = New-Object System.Net.WebClient

    # Download the installer
    $webClient.DownloadFile($downloadUrl, $destinationPath)

    Write-Host ".NET SDK downloaded successfully to: $destinationPath"
    exit 0
}
catch {
    Write-Error "Failed to download .NET SDK: $_"
    # Print more diagnostic information
    Write-Host "Download URL: $downloadUrl"
    Write-Host "Destination: $destinationPath"
    Write-Host "Exception details: $($_.Exception)"
    exit 1
}