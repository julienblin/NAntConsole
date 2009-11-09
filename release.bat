@echo off
CHOICE /C NY /M "Creating a release will cause auto-updates on every installed client. Do you want to continue ?" 
IF ERRORLEVEL 2 GOTO CONFIRMED
GOTO END

:CONFIRMED
@echo on
Dependencies\nant-0.86\NAnt.exe -buildfile:NAntConsole.build release
pause

:END