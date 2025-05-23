$ssotmePath = Join-Path $PSScriptRoot "testing/ssotme.json"

# Parse JSON and extract baseId
try {
    $jsonContent = Get-Content $ssotmePath -Raw | ConvertFrom-Json
    $baseIdSetting = $jsonContent.ProjectSettings | Where-Object { $_.Name -eq "baseId" }
    if (-not $baseIdSetting) {
        throw "baseId not found in ssotme.json"
    }
    $baseId = $baseIdSetting.Value
} catch {
    Write-Host "Failed to read or parse ssotme.json: $_"
    exit 1
}

# Construct dynamic URI using the extracted baseId
$uri = "https://ssotme-cli-airtable-bridge-ahrnz660db6k4.aws-us-east-1.controlplane.us/check?baseId=$baseId"

$lastChangedTime = $null
$changeEverDetected = $false

function Check-Changed {
    try {
        $response = Invoke-RestMethod -Uri $uri -Method Get
        return $response.changed -eq "true"
    } catch {
        Write-Host "Error fetching status: $_"
        return $false
    }
}

Write-Host "URI: $uri"
Write-Host "Listening for changes.." -NoNewline
while ($true) {
    if (Check-Changed) {
        $lastChangedTime = Get-Date
        $changeEverDetected = $true
        Write-Host "Change detected at $lastChangedTime"
    } else {
        if ($changeEverDetected -and $lastChangedTime) {
            $now = Get-Date
            $elapsed = ($now - $lastChangedTime).TotalSeconds

            Write-Host "No changes detected. Last change at $lastChangedTime. Elapsed: $([math]::Round($elapsed,1))s"

            if ($elapsed -gt 10) {
                Write-Host "10 seconds since last change. Running build..."
                cd testing
                ssotme -build
                cd docs
                ssotme -build
                cd ../dotnet-starter
                ssotme -build
                cd ../python
                ssotme -build
                cd ../..
                $changeEverDetected = $false  # reset until next change
            }
        } else {
            Write-Host -NoNewline "."
        }
    }

    Start-Sleep -Seconds 3
}
cd ..