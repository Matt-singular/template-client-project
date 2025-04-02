function Save-CodeCoverageSolutionReport {
    param (
        [string]$Name, 
        [string]$Path
    )

    $scriptPath = $PSScriptRoot # Automation\PowerShellModules
    $coverageReport = "$scriptPath\..\CodeCoverage\Report\$Name"
    $testResults = "$scriptPath\..\CodeCoverage\Report\TestResults\$Name"
   
    dotnet test `
        "$Path" `
        --collect:"XPlat Code Coverage" `
        --results-directory "$testResults"

    if (-not (Test-Path -Path $testResults)) {
        Write-Host "Test Results not present for $Name, exiting..." -ForegroundColor Yellow
        return
    }

    reportgenerator `
    -reports:"$testResults\*\coverage.cobertura.xml" `
    -targetdir:$coverageReport `
    -reporttypes:Html

    Write-Host "Completed code coverage for $Name" -ForegroundColor Green
}

function Save-CodeCoverageSummaryReport {
    $scriptPath = $PSScriptRoot # Automation\PowerShellModules
    $coverageReport = Resolve-Path "$scriptPath\..\CodeCoverage\Report" # Automation\CodeCoverage\Report
    $testResults = "$coverageReport\TestResults" # Automation\CodeCoverage\Report\TestResults
    $customReportPlugin = Resolve-Path "$scriptPath\CustomReports.dll" # Automation\PowerShellModules\CustomReports.dll

    if (-not (Test-Path -Path $testResults)) {
        Write-Host "Test Results not present for Summary Report, exiting..." -ForegroundColor Yellow
        return
    }

    reportgenerator `
        -reports:"$testResults\*\*\coverage.cobertura.xml" `
        -targetdir:$coverageReport `
        -reporttypes:LandingPage `
        -title:"Application Code Coverage" `
        -plugins:$customReportPlugin

    Write-Host "Completed code coverage for Summary Report" -ForegroundColor Green
}

Export-ModuleMember -Function Save-CodeCoverageSolutionReport
Export-ModuleMember -Function Save-CodeCoverageSummaryReport