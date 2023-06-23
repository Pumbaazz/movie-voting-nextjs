const express = require('express');
const { createProxyMiddleware } = require('http-proxy-middleware');
const NodeCache = require('node-cache');
const next = require('next');
require('dotenv').config();

const dev = process.env.NODE_ENV !== 'production';
const app = next({ dev });
const handle = app.getRequestHandler();

const cacheDuration = 10; // Cache duration in seconds
const cache = new NodeCache({ stdTTL: cacheDuration });

// Proxy server options
const proxyOptions = {
    target: process.env.REACT_APP_BASE_URL, // Replace with your target URL
    changeOrigin: true,
    onProxyRes: (proxyRes, req, res) => {
        if (proxyRes.statusCode === 200) {
            const cacheKey = req.originalUrl || req.url;
            const responseData = [];

            proxyRes.on('data', (chunk) => {
                responseData.push(chunk);
            });

            proxyRes.on('end', () => {
                const responseBody = Buffer.concat(responseData).toString('utf8');
                cache.set(cacheKey, responseBody);
            });
            console.log("🚀 ~ file: server.js:34 ~ cache.stats:", cache.stats)
        }
    },
};

// Create the proxy middleware
const proxyMiddleware = createProxyMiddleware(proxyOptions);

// Start Next.js app
app.prepare().then(() => {
    const server = express();

    // Apply the proxy middleware for API requests
    server.use('/api', (req, res, next) => {
        // Check if the response is cached
        const cacheKey = req.originalUrl || req.url;
        const cachedResponse = cache.get(cacheKey);
        if (cachedResponse) {
            res.send(cachedResponse);
            return;
        }
        next();
    }, proxyMiddleware);

    // Let Next.js handle all other routes
    server.all('*', (req, res) => {
        return handle(req, res);
    });

    // Start the proxy server
    server.listen(3000, (err) => {
        if (err) throw err;
        console.log('Next.js app with proxy server is running on port 3000');
    });
});
