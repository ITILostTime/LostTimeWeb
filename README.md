# LostTimeWeb

IT project @IN'TECH 2017mS5

## Build & Launch 

There is two mode to launch the project, developpement or production mode. Here is the process for developpement mode. To build and launch as production, just run the launch_prod.bat at the root of the project.

For developpement mode : using a PowerShell or Shell inside the root folder type the following command :

1. DotNet

:warning: if developpement stage, run :
``` bash
$Env:ASPNETCORE_ENVIRONMENT = "Development"
```
To change the environnement variable. We have to solve the bug on the launchconfig.json then run,
``` bash
cd ./src/LostTimeWeb.WebApp/

dotnet restore

dotnet run
```
2. SPA js

To restore packages, deploy, pack the app.js, and serve the app, launch another Shell and type :
``` bash
yarn 

yarn run dev
```
The dotnet application is called on http://localhost:5000 and the SPA is called by the dotnet application at http://localhost:8080 (and hot served, no need to reload) 

You can also use the launch_dev.bat batch file if you are on a windows env :wink:

## Deploy

Use the NuGet package and the dist folder (respectivly the .Net application and the .js SPA) a batch will be added soon to deploy auto-magically the project

## Technologies

.NET, C#, vue.js, node.js, Travis-CI, git, coffee, blood and tears

Readme writed with [markdown](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet#code) for Github and with a few [emoji](https://www.webpagefx.com/tools/emoji-cheat-sheet/)

## Members
Based on DotNetSample, following developpement of the webapp by
Pierre Koré & Quentin Vanbutsele

