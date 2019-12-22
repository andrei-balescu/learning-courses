// builds complete output path
let path = require('path');

// name has to be 'CleanWebpackPlugin' (bug?)
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const CopyPlugin = require('copy-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

module.exports = {
    // input file entry points
    entry: {
        site: ["./js/site.js", "./css/site.css"]
    },
    // output file
    output: {
        filename: "js/[name].js",
        path: path.resolve(__dirname, "../wwwroot")
    },
    plugins: [
        // clean up output folder
        new CleanWebpackPlugin(),
        new CopyPlugin([
            // to path relative to module.exports.output.path
            { from: "node_modules/jquery", to: "lib/jquery/" },
            { from: "node_modules/jquery-validation", to: "lib/jquery-validation/" },
            { from: "node_modules/jquery-validation-unobtrusive", to: "lib/jquery-validation-unobtrusive/" },
            { from: "node_modules/bootstrap", to: "lib/bootstrap/" }
        ]),
        new MiniCssExtractPlugin({
            // override parameters from module.exports.output
            filename: "css/[name].css"
        })
    ],
    module: {
        rules: [{
            // filter input file extension
            test: /\.css$/,
            // loaders executed in reverse order
            use: [
                {
                    loader: MiniCssExtractPlugin.loader
                },
                "css-loader"
            ]
        }]
    },
    optimization: {
        minimizer: []
    }
};