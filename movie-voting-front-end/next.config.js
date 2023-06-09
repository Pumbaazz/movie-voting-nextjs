/** @type {import('next').NextConfig} */

module.exports = {
    async rewrites() {
        return [
            {
                source: '/api/:path*',
                destination: `${process.env.REACT_APP_BASE_URL}/api/:path*`,
            }
        ];
    },
};