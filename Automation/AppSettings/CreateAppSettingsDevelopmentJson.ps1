# Paths
$scriptPath = $PSScriptRoot; # Root\Automation\AppSettings
$applicationPath = Resolve-Path (Join-Path $scriptPath '..\..\Server\Application.API') # Root\Server\Application.API
$templateFilePath = Resolve-Path (Join-Path $scriptPath 'Templates\AppsettingsDevelopmentTemplate.json') # Root\Automation\AppSettings\Templates\AppsettingsDevelopmentTemplate.json

# Generate AppSettings.Development.json
copy-item -path $templateFilePath -destination "$applicationPath\appsettings.Development.json" -confirm:$false
Write-Host "Created appsettings.Development.json" -ForegroundColor Green