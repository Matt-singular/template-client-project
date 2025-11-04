function Write-Success {
    param(
        [Parameter(Mandatory, Position=0)]
        [string]$Message
    )

    if ($?) { Write-Host $Message -ForegroundColor Green }
}

function Write-Failure {
    param(
        [Parameter(Mandatory, Position=0)]
        [string]$Message
    )

    if (-not $?) { Write-Host $Message -ForegroundColor Red }
}

function Write-Result {
    param(
        [Parameter(Mandatory, Position=0)]
        [string]$SuccessMessage,

        [Parameter(Mandatory, Position=1)]
        [string]$FailureMessage
    )

    if ($?) 
    { 
        Write-Host $SuccessMessage -ForegroundColor Green 
    }
    else
    {
        Write-Host $FailureMessage -ForegroundColor Red
    }
}

Export-ModuleMember -Function Write-Success, Write-Failure, Write-Result