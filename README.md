# LostTimeWeb

IT project @IN'TECH 2017mS5

## Launch

Using a PowerShell or Shell inside the base folder :

1. DotNet

:warning: if developpement stage, run :
```
$Env:ASPNETCORE_ENVIRONMENT = "Development"
```
To change the environnement variable. We have to solve the bug on the launchconfig.json then run,
```
cd ./src/ITI.PrimarySchool.WebApp/

dotnet restore

dotnet run
```
2. SPA js

To restore packages, deploy, pack the app.js, and serve the app, launch another Shell and type :
```
yarn 

yarn run dev
```
The dotnet application is called on http://localhost:5000 and the SPA is called by the dotnet application at http://localhost:8080 (and hot served, no need to reload) 

## Technology

.NET, C#, vue.js, node.js, Travis-CI, git, coffee, blood and tears

Readme writed with [markdown](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet#code) for Github and with a few [emoji](https://www.webpagefx.com/tools/emoji-cheat-sheet/)

## Members
Based on DotNetSample, following developpement of the webapp by
Pierre Koré, Quentin Vanbutsele

