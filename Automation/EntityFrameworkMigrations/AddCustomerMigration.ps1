# User Input
param (
    [Parameter(Mandatory = $true)]
    [string]$migrationName,
    [Parameter(Mandatory = $true)]
    [string]$customerName
)

# Paths
$scriptPath = $PSScriptRoot; # Root\Automation\EntityFrameworkMigrations
$solutionPath = Resolve-Path (Join-Path $scriptPath '..\..\Server'); # Root\Server

# Generate Product Migration
Set-Location $solutionPath
dotnet ef migrations add $migrationName --project .\Business.Infrastructure\ --context AppDbContext -o Migrations/Customers/$customerName