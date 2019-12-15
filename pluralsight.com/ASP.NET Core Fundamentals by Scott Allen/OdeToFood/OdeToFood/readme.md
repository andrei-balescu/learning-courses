# Ode to food web application

## Installing client side libraries
### Setting up client side development tools
* Install NPM  js package manager: `sudo apt-get install npm`
* Create package.json file that keeps track of installed packages: `npm init`. 
    * Run command at the root of the web project.
* Use default settings at prompt
* Install webpack compiler as a development dependency only: `npm install webpack --save-dev`
* Install webpack plugit to copy NPM libraries to the wwwroot/lib folder: `npm install copy-webpack-plugin --save-dev`
* Configure webpack (see webpack.config.js)
* Configure _build_ script that targets webpack in package.json
* Build your project: `npm run build`

Run `npm install` to restore packages once configured in packages.json.
### Adding client side libraries
1. DataTables library with Bootstrap 4 support from https://datatables.net/: `npm install --save datatables.net-bs4`

