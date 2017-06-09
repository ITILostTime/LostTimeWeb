var path = require('path')
var webpack = require('webpack')
var ExtractTextPlugin = require("extract-text-webpack-plugin")
var CopyWebpackPlugin = require('copy-webpack-plugin');

var wwwroot = "../../wwwroot";
/*
function resolution (dir) {
  var out = path.join(__dirname, '..', dir)
  return out
}
*/
module.exports = {
  entry: './src/main.js',

  output: {
    path: path.resolve(wwwroot, './dist'),
    publicPath: process.env.NODE_ENV === 'production' ? '../dist/' : 'http://localhost:8080/dist/',
    filename: 'losttimeweb.js'
  },
  /*resolve: {
    extensions: ['.js', '.vue', '.json'],
    alias: {
      '@': resolution('src')
    }
  },*/
  module: {
    loaders: [
      {
        test: /\.vue$/,
        loader: 'vue-loader',
        options: {
          loaders: {
            css: ExtractTextPlugin.extract("css-loader"),
            less: ExtractTextPlugin.extract("css-loader!less-loader")
          }
        }
      },
      {
        test: /\.js$/,
        loader: 'babel-loader',
        exclude: /node_modules/
      },
      {
        test: /\.(png|jpg|gif|svg)$/,
        loader: 'file-loader', 
        query: {
          name: '[name].[ext]?[hash]'
        }
      },
      {
        test: /\.(woff2?|eot|ttf|otf)(\?.*)?$/,
        loader: 'file-loader',
        query: {
          name: '[name].[ext]?[hash]'
        }
      }
    ]
  },

  devServer: {
    headers: { "Access-Control-Allow-Origin": "*" },
    historyApiFallback: true,
    noInfo: true
  },

  watch: process.env.NODE_ENV === 'production' ? false : true,

  devtool: process.env.NODE_ENV === 'production' ? '#source-map' : '#eval-source-map',

  plugins: [
    new webpack.DefinePlugin({
      'process.env.NODE_ENV': JSON.stringify(process.env.NODE_ENV || 'development')
    }),

    // Bootstrap's Javascript is not module compliant, so we need to define the usual global variables of JQuery
    new webpack.ProvidePlugin({
      jQuery: 'jquery',
      $: 'jquery',
      jquery: 'jquery'
    }),

    new ExtractTextPlugin("style.css"),

    new CopyWebpackPlugin([
            // Copy directory contents to {output}/to/directory/
            { from: 'dist/img/carrousel/' }, 
      ], {
      copyUnmodified: true
    })
  ]
}

if (process.env.NODE_ENV === 'production') {
  module.exports.plugins = (module.exports.plugins || []).concat([
    new webpack.optimize.UglifyJsPlugin({
      compress: {
        warnings: false
      }
    })
  ])
}
