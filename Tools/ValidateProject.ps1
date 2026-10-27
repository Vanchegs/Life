param(
    [string]$UnityPath = 'C:/Program Files/Unity/Hub/Editor/6000.4.3f1/Editor/Unity.exe'
)

$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path $PSScriptRoot -Parent
$validationRoot = Join-Path $projectRoot '.utmp/CodeReviewValidation'
New-Item -ItemType Directory -Path $validationRoot -Force | Out-Null
foreach ($folder in 'Assets', 'Packages', 'ProjectSettings') {
    Copy-Item -LiteralPath (Join-Path $projectRoot $folder) -Destination $validationRoot -Recurse -Force
}
$editorPath = Join-Path $validationRoot 'Assets/Editor'
New-Item -ItemType Directory -Path $editorPath -Force | Out-Null
Copy-Item -LiteralPath (Join-Path $PSScriptRoot 'Validation/CodeReviewValidation.cs') -Destination $editorPath -Force
$settingsPath = Join-Path $validationRoot 'ProjectSettings/ProjectSettings.asset'
$settings = [IO.File]::ReadAllText($settingsPath)
$settings = [regex]::Replace($settings, '(?m)^  companyName:.*$', '  companyName: CodexValidation')
$settings = [regex]::Replace($settings, '(?m)^  productName:.*$', "  productName: LifeValidation-$([guid]::NewGuid().ToString('N'))")
[IO.File]::WriteAllText($settingsPath, $settings)
$logPath = Join-Path $validationRoot 'validation.log'
$arguments = @('-batchmode', '-nographics', '-projectPath', ('"' + $validationRoot + '"'), '-executeMethod', 'CodeReviewValidation.Run', '-logFile', ('"' + $logPath + '"'))
$process = Start-Process -FilePath $UnityPath -ArgumentList $arguments -WindowStyle Hidden -PassThru
$process.WaitForExit()
if ($process.ExitCode -ne 0 -or !(Select-String -LiteralPath $logPath -SimpleMatch 'CODE_REVIEW_VALIDATION_PASSED' -Quiet)) {
    Get-Content -LiteralPath $logPath -Tail 60
    throw "Validation failed. Log: $logPath"
}
Select-String -LiteralPath $logPath -Pattern 'VALIDATED:|CODE_REVIEW_VALIDATION_PASSED' | ForEach-Object Line
