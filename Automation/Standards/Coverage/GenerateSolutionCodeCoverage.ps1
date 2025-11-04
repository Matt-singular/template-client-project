# Modules
$modulePath = Resolve-Path (Join-Path $PSScriptRoot '..\..\Common\Modules')
Import-Module "$modulePath\Logging.psm1"
Import-Module "$modulePath\Navigation.psm1"
Import-Module "$modulePath\Coverage.psm1"

# Paths
$serverPath = Resolve-Path (Find-Up -FolderName 'Server')
$resultsPath = Join-Path $PSScriptRoot 'Results'
$reportPath = Join-Path $PSScriptRoot 'Report'

# Generate project code coverage
function GenerateCodeCoverage {
    # 1. Generate code coverage report (solution)
    Save-CodeCoverageReport "Application" $serverPath $resultsPath $reportPath

    # 2. Generate code coverage report (summary)
    Save-CodeCoverageSummary $resultsPath $reportPath
}

GenerateCodeCoverage
Write-Success 'Successfully generated code coverage reports'