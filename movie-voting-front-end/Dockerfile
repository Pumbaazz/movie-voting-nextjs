# Install dependencies only when needed
FROM node:18 as deps
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY . .

# Rebuild the source code only when needed
FROM node:18
WORKDIR /app
COPY . .
COPY --from=deps /app ./
RUN npm run build

EXPOSE 3000
CMD ["npm", "run", "start"]