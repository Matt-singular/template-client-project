function Write-Success {
    param([string]$Message)
    if ($?) { Write-Host $Message -ForegroundColor Green }
}

function Write-Error {
    param([string]$Message)
    if (-not $?) { Write-Host $Message -ForegroundColor Red }
}

Export-ModuleMember -Function Write-Success, Write-Error