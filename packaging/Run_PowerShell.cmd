@echo off
rem SCHomeGuard - run without the exe (asks for administrator rights)
powershell.exe -NoProfile -ExecutionPolicy Bypass -STA -File "%~dp0SCHomeGuard.ps1"
