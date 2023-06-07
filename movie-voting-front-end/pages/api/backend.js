const express = require('express');
const createProxyMiddleware = require('http-proxy-middleware');

const app = express();

// process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";

// Proxy API requests to your ASP.NET Web API backend
app.use(
    '/api',
    createProxyMiddleware({
        target: 'http://localhost:5005', // Replace with your ASP.NET Web API backend URL and port
        changeOrigin: true,
        pathRewrite: {
            '^/api': '/api', // Adjust the rewrite path if necessary
        },
    })
);

module.exports = app;