@echo off
del ..\nuget\*.nupkg

nuget Pack ..\nuget\Xander.PasswordValidator.nuspec -outputdirectory ..\nuget\ -NonInteractive
nuget Pack ..\nuget\Xander.PasswordValidator.Web.nuspec -outputdirectory ..\nuget\ -NonInteractive

IF "%1" == "-push" (
nuget Push ..\nuget\*.nupkg
)