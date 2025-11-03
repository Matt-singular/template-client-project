# Paths
clear
$scriptPath = $PSScriptRoot; # Root\Automation\DotnetFormatting
$solutionPath = Resolve-Path (Join-Path $scriptPath '..\..\Server') # Root\Server

Set-Location $solutionPath
dotnet format --include-generated --severity info --verbosity diagnostic
#dotnet format --include-generated --exclude ..\Client\Application.GUI --severity info --verbosity diagnostic
#dotnet format whitespace --verbosity diagnostic
#dotnet format style --verbosity diagnostic
#dotnet format analyzers --verbosity diagnostic --severity error