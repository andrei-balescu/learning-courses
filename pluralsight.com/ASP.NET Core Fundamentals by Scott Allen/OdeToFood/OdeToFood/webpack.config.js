// builds complete output path
let path = require('path');

const CopyPlugin = require('copy-webpack-plugin');

module.exports = {
    // application entry point
    entry: "./wwwroot/js/site.js",
    // output file
    output: {
        filename: "dist/js/site.bundle.js",
        path: path.resolve(__dirname, "wwwroot")
    },
    plugins: [
        new CopyPlugin([
            // to path relative to module.exports.output.path
            { from: "node_modules/datatables.net", to: "lib/datatables.net/" },
            { from: "node_modules/datatables.net-bs4", to: "lib/datatables.net-bs4/" }
        ])
    ]
};