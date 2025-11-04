Import-Module (Join-Path $PSScriptRoot '..\Modules\Logging.psm1')
Import-Module (Join-Path $PSScriptRoot '..\Modules\Navigation.psm1')

function Save-CodeCoverageReport {
    param(
        [Parameter(Mandatory, Position=0)]
        [string]$Name,

        [Parameter(Mandatory, Position=1)]
        [string]$Path,

        [Parameter(Position=2)]
        [string]$ResultsPath = $null,

        [Parameter(Position=3)]
        [string]$ReportPath = $null
    )

    # Paths
    $standardsPath = Find-Up -FolderName 'Standards'
    if (-not [string]::IsNullOrEmpty($ResultsPath)) { Join-Paths $standardsPath 'Coverage\Results' }
    if (-not [string]::IsNullOrEmpty($ReportPath)) { Join-Paths $standardsPath 'Coverage\Report' }

    # 1. Create/clear the pre-requisite folders
    New-Folder $ResultsPath
    New-Folder $ReportPath

    # 2. Gather unit test results using Coverlet
    dotnet test $Path --collect:"XPlat Code Coverage" --results-directory $ResultsPath
    if (-not (Test-Path -Path $ResultsPath)) {
        Write-Error "Test Results not present for $Name"
        return
    }

    # 3.Generate the coverage report
    reportgenerator `
    -reports:"$ResultsPath\*\coverage.cobertura.xml" `
    -targetdir:"$ReportPath\$Name" `
    -reporttypes:Html

    Write-Success "Completed code coverage for $Name"
}

function Save-CodeCoverageSummary {
    param(
        [Parameter(Position=0)]
        [string]$ResultsPath = $null,

        [Parameter(Position=1)]
        [string]$ReportPath = $null
    )

    # Paths
    $standardsPath = Find-Up -FolderName 'Standards'
    $utilsPath = Find-Up -FolderName 'Utils'
    if (-not [string]::IsNullOrEmpty($ResultsPath)) { Join-Paths $standardsPath 'Coverage\Results' }
    if (-not [string]::IsNullOrEmpty($ReportPath)) { Join-Paths $standardsPath 'Coverage\Report' }
    $customReportPlugin = Join-Paths $utilsPath 'CustomReports.dll'

    # 1. Validate that test results are present
    if (-not (Test-Path -Path $ResultsPath)) {
        Write-Error "Test Results not present for Summary Report..."
        return
    }

    # 2. Generate the custom summary report
    reportgenerator `
        -reports:"$ResultsPath\*\coverage.cobertura.xml" `
        -targetdir:$ReportPath `
        -reporttypes:LandingPage `
        -title:"Application Code Coverage" `
        -plugins:$customReportPlugin
}

Export-ModuleMember -Function Save-CodeCoverageReport, Save-CodeCoverageSummary