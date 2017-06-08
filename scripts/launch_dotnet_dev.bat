@echo off
echo [45m[37mLostTime .NET server[0m
cd ..\src\LostTimeWeb.WebApp
set ASPNETCORE_ENVIRONMENT=Development
echo Environnement set to %ASPNETCORE_ENVIRONMENT%
echo [45m[37m.Net Package Restore...[0m
dotnet restore
echo [45m[37m.Net Run app...[0m
dotnet run
exit