# Paths
$scriptPath = $PSScriptRoot; # Root\Automation\EntityFrameworkMigrations
$solutionPath = Resolve-Path (Join-Path $scriptPath '..\..\Server') # Root\Server

# Execute Database Migrations
Set-Location $solutionPath
dotnet ef database update --project .\Business.Infrastructure\ --context AppDbContext