# Runnig ASP.NET Core website in a Docker container from a volume
## Setting up an ASP.NET Core demo website 
Create demo website: `dotnet new mvc`
Move client development to separate folder: `mkdir Client && mv wwwroot/js/ Client/js && mv wwwroot/css/ Client/css`
Initialize client folder: `cd Client/ && npm init`
Install dependent libraries to client folder: `npm install bootstrap jquery jquery-validation jquery-validation-unobtrusive`
Istall build dependencies to client folder: `npm install --save-dev webpack copy-webpack-plugin clean-webpack-plugin mini-css-extract-plugin css-loader`
Configure build scripts. See `webpack.config.js` & `package.json` (`scripts` section)
Publish website: `npm run build --prefix=Client && dotnet publish`
