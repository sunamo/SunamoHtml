# Remove EN/CZ bilingual comments from all .cs files in the project

$projectRoot = "E:\vs\Projects\PlatformIndependentNuGetPackages\SunamoHtml"
$csFiles = Get-ChildItem -Path $projectRoot -Filter "*.cs" -Recurse | Where-Object {
    $_.FullName -notmatch '\\bin\\' -and $_.FullName -notmatch '\\obj\\'
}

$totalFiles = 0
$modifiedFiles = 0

foreach ($file in $csFiles) {
    $totalFiles++
    $content = Get-Content $file.FullName -Raw
    $originalContent = $content

    # Remove lines starting with // EN: or // CZ:
    $content = $content -replace '(?m)^\s*// EN:.*$\r?\n?', ''
    $content = $content -replace '(?m)^\s*// CZ:.*$\r?\n?', ''

    # Remove empty lines that were left behind (max 2 consecutive empty lines)
    $content = $content -replace '(\r?\n){3,}', "`r`n`r`n"

    if ($content -ne $originalContent) {
        Set-Content -Path $file.FullName -Value $content -NoNewline
        $modifiedFiles++
        Write-Host "Modified: $($file.FullName)"
    }
}

Write-Host ""
Write-Host "Total files processed: $totalFiles"
Write-Host "Files modified: $modifiedFiles"
