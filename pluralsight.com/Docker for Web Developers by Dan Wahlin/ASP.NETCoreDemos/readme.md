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
* Rune application: `dotnet run`
