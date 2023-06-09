import express from 'express';
import createProxyMiddleware from 'http-proxy-middleware';

const app = express();

// const API_URL = "http://localhost:5005";
process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";

// Proxy API requests to your ASP.NET Web API backend
app.use(
    '/api',
    createProxyMiddleware({
        target: process.env.REACT_APP_BASE_URL,
        changeOrigin: true,
        pathRewrite: {
            '^/api': '/api',
        },
    })
);

export default app;