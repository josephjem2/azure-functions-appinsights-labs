[CmdletBinding()]
param()

$ErrorActionPreference = "Stop"

Write-Host "Checking local prerequisites for Azure Functions demo..." -ForegroundColor Cyan

$funcVersion = func --version
$dotnetVersion = dotnet --version

Write-Host "func version: $funcVersion"
Write-Host "dotnet version: $dotnetVersion"

if (-not (Get-Command az -ErrorAction SilentlyContinue)) {
    Write-Error "Azure CLI (az) is not installed or not in PATH."
}

Write-Host "Azure CLI detected." -ForegroundColor Green
Write-Host "Prerequisite check completed." -ForegroundColor Green
