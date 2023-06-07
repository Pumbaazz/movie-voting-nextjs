/** @type {import('next').NextConfig} */

const { createProxyMiddleware } = require('http-proxy-middleware');

// const nextConfig = {}
// module.exports = nextConfig
module.exports = {
    async rewrites() {
        return [
            {
                source: '/api/:path*',
                destination: `${process.env.REACT_APP_BASE_URL}/api/:path*`,
            }
        ];
    },
    async middleware() {
        const proxy = createProxyMiddleware('/api', {
            target: `${process.env.REACT_APP_BASE_URL}`,
            changeOrigin: true,
            pathRewrite: {
                '^/api': '',
            },
        });

        return {
            '/api': proxy,
        };
    },
};