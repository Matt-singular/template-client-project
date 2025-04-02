# Paths
$scriptPath = $PSScriptRoot # Automation\CodeCoverage
$rootPath = Resolve-Path "$scriptPath\..\..\" # Root
$powerShellModulesPath = Resolve-Path "$scriptPath\..\PowerShellModules" # Automation\PowerShellModules
$coverageReport = Resolve-Path (Join-Path $scriptPath "Report") # Automation\CodeCoverage\Report

# Import PowerShell Modules
Import-Module (Resolve-Path "$powerShellModulesPath\CodeCoverageModule.psm1")

# List of solutions and/or project to pull code coverage for
$solutions = @(
    @{ Name = "Applications"; Path = (Resolve-Path "$rootPath") }
)

# Clear out the code coverage report and test results
if (Test-Path -Path $coverageReport) {
    Get-ChildItem -Path $coverageReport | Remove-Item -Recurse -Force
}

# Generate code coverage report for each solution specified
foreach ($solution in $solutions) {
    Save-CodeCoverageSolutionReport `
         -Name $solution.Name `
         -Path $solution.Path
}

# Generate code coverage summary report
Save-CodeCoverageSummaryReport