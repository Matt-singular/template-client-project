function Find-Up {
    param(
        [Parameter(Position=0)]
        [string]$StartPath = $PSScriptRoot,
        
        [Parameter(Mandatory, Position=1)]
        [string]$FolderName
    )

    $current = Resolve-Path $StartPath -ErrorAction Stop | Select-Object -ExpandProperty ProviderPath

    while ($current -ne [System.IO.Path]::GetPathRoot($current)) {
        $candidate = Join-Path $current $FolderName
        if (Test-Path $candidate -PathType Container) {
            return $candidate
        }
        $current = Split-Path $current -Parent
    }

    return $null
}

function Join-Paths {
    param(
        [Parameter(Mandatory, Position=0)]
        [string]$PathA,

        [Parameter(Position=1)]
        [string]$PathB
    )

    if (-not [string]::IsNullOrEmpty($PathB)) {
        return Resolve-Path (Join-Path $PathA $PathB)
    }

    return Resolve-Path (Join-Path $PathA)
}

function New-Folder {
    param(
        [Parameter(Mandatory, Position=0)]
        [string]$FolderName
    )

    if (Test-Path -Path $FolderName) {
        Resolve-Path $FolderName
        Get-ChildItem -Path $FolderName | Remove-Item -Recurse -Force | Out-Null

        return
    }

    New-Item -Path $FolderName -ItemType Directory -Force | Out-Null
    Resolve-Path $FolderName
}

Export-ModuleMember -Function Find-Up, Join-Paths, New-Folder