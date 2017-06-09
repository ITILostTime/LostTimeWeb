@echo off
cd scripts
echo Launching LostTime in PRODUCTION mode
echo Build LostTime SPA via Webpack
start cmd /c build_spa_prod.bat
echo Launching LostTime .NET server
start cmd /k launch_dotnet_prod.bat