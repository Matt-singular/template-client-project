# Modules
$modulePath = Resolve-Path (Join-Path $PSScriptRoot '..\..\Common\Modules')
Import-Module "$modulePath\Logging.psm1"
Import-Module "$modulePath\Navigation.psm1"

# Paths
$server = Find-Up -FolderName 'Server'
Set-Location $server

# Formatting
dotnet format --include-generated --severity info --verbosity diagnostic
#dotnet format --include-generated --exclude ..\Client\Application.GUI --severity info --verbosity diagnostic
#dotnet format whitespace --verbosity diagnostic
#dotnet format style --verbosity diagnostic
#dotnet format analyzers --verbosity diagnostic --severity error
Write-Result 'Successfully formatted solution' 'Failed to format solution'