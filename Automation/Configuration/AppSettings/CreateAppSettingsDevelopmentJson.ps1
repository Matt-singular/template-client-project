# Modules
$modulePath = Resolve-Path (Join-Path $PSScriptRoot '..\..\Common\Modules')
Import-Module "$modulePath\Logging.psm1"
Import-Module "$modulePath\Navigation.psm1"

# Paths
$serverPath = Resolve-Path (Find-Up -FolderName 'Server')
$applicationPath = Join-Paths $serverPath 'Application.API'
$appsettingsPath = Join-Paths $applicationPath 'appsettings.Development.json'
$templatePath = Join-Paths $PSScriptRoot 'AppsettingsDevelopmentTemplate.json'

# Generate AppSettings.Development.json
Copy-Item -Path $templatePath -Destination $appsettingsPath -Confirm:$false
Write-Success 'Created appsettings.Development.json'