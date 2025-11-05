function Save-CodeCoverageSolutionReport {
    param (
        [string]$Name, 
        [string]$Path
    )

    $scriptPath = $PSScriptRoot # Automation\PowerShellModules
    $coverageReport = "$scriptPath\..\CodeCoverage\Report" # Automation\CodeCoverage\Report
    $testResults = "$coverageReport\TestResults" # Automation\CodeCoverage\Report\TestResults
   
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
    -targetdir:"$coverageReport\$Name" `
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

    Write-Host "Completed code coverage for summary report" -ForegroundColor Green
}

function Save-CodeCoverageSummaryReportShortcut {
    param (
        [string]$Name
    )

    $scriptPath = $PSScriptRoot # Automation\PowerShellModules
    $coverageReport = Resolve-Path "$scriptPath\..\CodeCoverage\Report" # Automation\CodeCoverage\Report
    $target = "$coverageReport\$Name\index.html" # Automation\CodeCoverage\Report\Application\index.html
    $shortcutPath = Join-Path $coverageReport "CoverageReport.lnk"

    $WshShell = New-Object -ComObject WScript.Shell
    $shortcut = $WshShell.CreateShortcut($shortcutPath)
    $shortcut.TargetPath = $target
    $shortcut.Save()

    Write-Host "Completed creation of code coverage summary report shorcut" -ForegroundColor Green
}

Export-ModuleMember -Function Save-CodeCoverageSolutionReport
Export-ModuleMember -Function Save-CodeCoverageSummaryReport
Export-ModuleMember -Function Save-CodeCoverageSummaryReportShortcut