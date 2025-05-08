# DownloadDotNet.ps1
# Script to download the .NET SDK installer

$dotnetVersion = "7.0.410"
$downloadUrl = "https://download.visualstudio.microsoft.com/download/pr/9c3e1dcb-e4f6-4d6c-b29c-2ac9d34bf571/47978b5f8827a01e8ddb5697e56a3abe/.NET_SDK_$(dotnetVersion)_win_x64.exe"
$destinationPath = [System.IO.Path]::Combine($env:TEMP, "dotnet-sdk-$dotnetVersion-win-x64.exe")

# Ensure TLS 1.2 is used
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

try {
    Write-Host "Downloading .NET SDK $dotnetVersion..."
    
    # Create a web client
    $webClient = New-Object System.Net.WebClient
    
    # Download the installer
    $webClient.DownloadFile($downloadUrl, $destinationPath)
    
    Write-Host ".NET SDK downloaded successfully to: $destinationPath"
    
    # Return the path to the installer
    return $destinationPath
}
catch {
    Write-Error "Failed to download .NET SDK: $_"
    exit 1
}