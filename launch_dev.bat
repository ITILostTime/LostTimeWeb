@echo off
cd scripts
echo Launching LostTime in DEVELOPPEMENT mode
echo Launching LostTime SPA via Webpack
start cmd /k launch_spa_dev.bat
echo Launching LostTime .NET server
start cmd /k launch_dotnet_dev.bat