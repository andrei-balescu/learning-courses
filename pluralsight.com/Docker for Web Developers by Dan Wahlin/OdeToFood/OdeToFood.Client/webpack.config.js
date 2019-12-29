let path = require('path');

const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const CopyPlugin = require('copy-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const OptimizeCSSAssetsPlugin = require('optimize-css-assets-webpack-plugin');
const TerserPlugin= require('terser-webpack-plugin');

var isProdEnv = process.env.NODE_ENV === 'production';
var outputDir = process.env.OUTPUT_DIR || "../OdeToFood/wwwroot";

module.exports = {
    entry: {
        site: ["./js/site.js", "./css/site.css"]
    },
    output: {
        filename: "js/[name].bundle.js",
        path: path.resolve(__dirname, outputDir)
    },
    plugins: [
        new CleanWebpackPlugin(),
        new CopyPlugin([
            { from: "node_modules/jquery", to: "lib/jquery/" },
            { from: "node_modules/jquery-validation", to: "lib/jquery-validation/" },
            { from: "node_modules/jquery-validation-unobtrusive", to: "lib/jquery-validation-unobtrusive/" },
            { from: "node_modules/bootstrap", to: "lib/bootstrap/" },
            { from: "node_modules/datatables.net", to: "lib/datatables.net/" },
            { from: "node_modules/datatables.net-bs4", to: "lib/datatables.net-bs4/" }
        ]),
        new MiniCssExtractPlugin({
            filename: "css/[name].bundle.css"
        })
    ],
    module: {
        rules: [{
            test: /\.css$/,
            use: [
                {
                    loader: MiniCssExtractPlugin.loader
                },
                "css-loader"
            ]
        }]
    },
    optimization: {
        minimizer: isProdEnv ? [ new TerserPlugin(), new OptimizeCSSAssetsPlugin() ] : []
    }
};