@echo off
del /Q ..\built-assemblies\*.*

copy ..\src\Xander.PasswordValidator\bin\release\*.* ..\built-assemblies
IF ERRORLEVEL 1 GOTO ERROR

copy ..\src\Xander.PasswordValidator.Web\bin\release\*.* ..\built-assemblies
IF ERRORLEVEL 1 GOTO ERROR

sn -R ..\built-assemblies\Xander.PasswordValidator.dll ..\..\keys\Xander.snk
IF ERRORLEVEL 1 GOTO ERROR

sn -R ..\built-assemblies\Xander.PasswordValidator.Web.dll ..\..\keys\Xander.snk
IF ERRORLEVEL 1 GOTO ERROR

GOTO END

:ERROR

echo There was an error and the script was stopped!

:END
