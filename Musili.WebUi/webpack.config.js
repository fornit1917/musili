const webpack = require("webpack");
const HtmlWebpackPlugin = require("html-webpack-plugin");
//const CopyWebpackPlugin = require('copy-webpack-plugin');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const OptimizeCSSAssetsPlugin = require("optimize-css-assets-webpack-plugin");
const cssnano = require("cssnano");

module.exports = function (env, options) {
    const isDev = options.mode === "development";

    var plugins = [
        new MiniCssExtractPlugin({
           filename: "css/[name].css",
        }),

        new HtmlWebpackPlugin({
            hash: true,
            template: "./src/site/templates/index.twig",
            filename: "index.html",
            chunks: ["site"],
        }),

        new HtmlWebpackPlugin({
            hash: true,
            template: "./src/admin/templates/index.twig",
            filename: "admin/index.html",
            chunks: ["admin"],
        }),

        //new CopyWebpackPlugin([
        //    { from: "img", to: "img" },
        //    { from: "charting_library", to: "charting_library" },
        //    { from: "datafeeds/udf/dist", to: "datafeeds/udf/dist" }
        //]),
    ];
    if (!isDev) {
        plugins.push(
            new webpack.DefinePlugin({
                "process.env": {
                    "NODE_ENV": JSON.stringify("production")
                }
            })
        );

        plugins.push(
           new OptimizeCSSAssetsPlugin({
               cssProcessor: cssnano,
               canPrint: false,
           })
        );
    }

    return {
        entry: {
            //"vendors-common-css": ["./src/css/vendor-common.scss"],
            "site": ["./src/site/scripts/index.ts", "./src/site/styles/site.scss"],
            "admin": ["./src/admin/scripts/index.ts"],
        },
        output: { path: `${__dirname}/dist`, filename: "js/[name].js" },
        resolve: {
            extensions: ['.tsx', '.ts', '.js']
        },
        //optimization: {
        //    splitChunks: {
        //        cacheGroups: {
        //            commons: {
        //                name: 'commons',
        //                chunks: 'initial',
        //                minChunks: 2
        //            }
        //        }
        //    },
        //},
        plugins: plugins,
        module: {
            rules: [
                {
                    test: /\.tsx?$/,
                    use: 'awesome-typescript-loader',
                    exclude: /node_modules/
                },

                {
                    test: /\.scss$/,
                    use: [
                        MiniCssExtractPlugin.loader,
                        "css-loader",
                        "sass-loader",
                    ],
                },

                {
                    test: /\.woff2?$|\.ttf$|\.eot$|\.svg$|\.png|\.jpe?g|\.gif$/,
                    loader: "file-loader",
                    options: {
                        name: "[name].[ext]",
                        //useRelativePath: true,
                        publicPath: "/",
                    }
                },                
                
                { test: /\.twig$/, loader: "twig-loader" }
            ],
        },
    };
};