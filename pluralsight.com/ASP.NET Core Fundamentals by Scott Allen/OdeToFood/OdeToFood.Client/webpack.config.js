// builds complete output path
let path = require('path');

// name has to be 'CleanWebpackPlugin' (bug?)
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const CopyPlugin = require('copy-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
// css minifier
const OptimizeCSSAssetsPlugin = require('optimize-css-assets-webpack-plugin');
// js minifier (override)
const TerserPlugin= require('terser-webpack-plugin');

module.exports = {
    // input file entry points
    entry: {
        site: ["./js/site.js", "./css/site.css"]
    },
    // output file
    output: {
        filename: "js/[name].bundle.js",
        path: path.resolve(__dirname, "../OdeToFood/wwwroot")
    },
    plugins: [
        // clean up output folder
        new CleanWebpackPlugin(),
        new CopyPlugin([
            // to path relative to module.exports.output.path
            { from: "node_modules/jquery", to: "lib/jquery/" },
            { from: "node_modules/jquery-validation", to: "lib/jquery-validation/" },
            { from: "node_modules/jquery-validation-unobtrusive", to: "lib/jquery-validation-unobtrusive/" },
            { from: "node_modules/bootstrap", to: "lib/bootstrap/" },
            { from: "node_modules/datatables.net", to: "lib/datatables.net/" },
            { from: "node_modules/datatables.net-bs4", to: "lib/datatables.net-bs4/" },
            // copy images
            { from: "images", to: "images/" }
        ]),
        new MiniCssExtractPlugin({
            // override parameters from module.exports.output
            filename: "css/[name].bundle.css"
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
        minimizer: [ new TerserPlugin(), new OptimizeCSSAssetsPlugin() ]
    }
};