# Runnig ASP.NET Core website in a Docker container from a volume
## Setting up an ASP.NET Core demo website 
* Create demo website: `dotnet new mvc`
* Move client development to separate folder: `mkdir Client && mv wwwroot/js/ Client/js && mv wwwroot/css/ Client/css`
* Initialize client folder: `cd Client/ && npm init`
* Install dependent libraries to client folder: `npm install bootstrap jquery jquery-validation jquery-validation-unobtrusive`
* Istall build dependencies to client folder: `npm install --save-dev webpack copy-webpack-plugin clean-webpack-plugin mini-css-extract-plugin css-loader`
* Configure build scripts. See `webpack.config.js` & `package.json` (`scripts` section)
* Build website: `npm run build --prefix=Client && dotnet build`

## Setting up website as a volume in a ASP.NET Docker image
* Change website url bindings from `localhost` to `0.0.0.0`, otherwise binding will fail inside the container (bug?)
    * See `profiles."ASP.NETCoreDemos".applicationUrl` in `Properties\launchSettings.json`
* Pull ASP.NET Core Docker image: `docker pull mcr.microsoft.com/dotnet/core/sdk`
* Run container in interactive mode: `docker run -it -p 8080:5001 -v "$(pwd)":/app -w "/app" mcr.microsoft.com/dotnet/core/sdk /bin/bash`
    * `-it` allows runnning commands inside terminal once started
    * `/bin/bash` runs a bash terminal inside container
* Build application: `dotnet build`
    * node.js not available inside container
* Run application: `dotnet run`
* To access site go to `https://localhost:8080` in browser

## Creating custom image for website
* Install `docker` extension for VS code
* (Optional) Create docker file in VS code: `F1` > `Docker: Add Docker Files to Workspace...`
    * Specify ports `5000,5001`
    * See `vscode.dockerfile`
    * Also adds VS code launch options / tasks
        * Change host port bindings in `tasks.json` in order to run target - does not work with defaults (80/443)
        * Container port bindings cannot be changed - build ignores ENV variable if set
### Dev image
* Create docker file (see `Dockerfile`)
* Build image: `docker build -t andrei.balescu/aspnetcore-demo:dev .` to build image
* Run image - swap container code with local code: `docker run -d -p 8080:5001 -v "$(pwd)":/app andrei.balescu/aspnetcore-demo:dev`
* To access site go to `https://localhost:8080` in browser
### Prod image
* Create docker file (see `prod.dockerfile`)
* Build image: `docker build -t andrei.balescu/aspnetcore-demo:prod -f prod.dockerfile .`
    * Can also build using VS code: `F1` > `Docker: Build Image`
* Run image: `docker run -d -p:8080:5001 andrei.balescu/aspnetcore-demo:prod`
