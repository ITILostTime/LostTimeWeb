@echo off
echo [46m[96mBuild LostTime SPA[0m
cd ..\src\LostTimeWeb.WebApp\App\losttimeweb
echo [46m[96mNode package restore...[0m
call yarn
echo [46m[96mbuild the webapp using webpack[0m
call yarn run build
exit