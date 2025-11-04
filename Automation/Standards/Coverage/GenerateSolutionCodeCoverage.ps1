# Paths
$scriptPath = $PSScriptRoot; # Root\Automation\CodeCoverage
$powerShellModulesPath = Resolve-Path "$scriptPath\..\PowerShellModules" # Automation\PowerShellModules
$applicationPath = Resolve-Path (Join-Path $scriptPath '..\..\Server\') # Root\Server
$coverageReportPath = "$scriptPath\Report" # Root\Automation\CodeCoverage\Report
$coverageReportTestsPath = "$coverageReportPath\TestResults" # Root\Automation\CodeCoverage\Report\TestResults
$applicationName = "Application"

# Import PowerShell Modules
Import-Module (Resolve-Path "$powerShellModulesPath\CodeCoverageModule.psm1")

# Create code coverage folders and ensure they're clear
New-Item -Path $coverageReportTestsPath -ItemType Directory -Force
if (Test-Path -Path $coverageReportPath) {
    Get-ChildItem -Path $coverageReportPath | Remove-Item -Recurse -Force
}

# Generate code coverage report for solution
Save-CodeCoverageSolutionReport `
    -Name $applicationName `
    -Path $applicationPath

# Generate code coverage summary report
Save-CodeCoverageSummaryReport

# Create Report shortcut
Save-CodeCoverageSummaryReportShortcut -Name $applicationName