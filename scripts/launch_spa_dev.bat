@echo off
echo [46m[96mLostTime SPA server[0m
cd ..\src\LostTimeWeb.WebApp\App\losttimeweb
echo [46m[96mNode package restore...[0m
call yarn
echo [46m[96mlaunch Node.js webpack dev server[0m
call yarn run dev
exit