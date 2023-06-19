# this code run back end ok
# # Frontend build stage
# FROM node:18-alpine AS build-frontend
# WORKDIR /app/movie-voting-front-end
# COPY movie-voting-front-end/package*.json ./
# RUN npm install
# COPY movie-voting-front-end/ ./
# RUN npm run build

# # Backend build stage
# FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-backend
# WORKDIR /app/movie-voting-back-end/
# COPY movie-voting-back-end/*.csproj ./
# RUN dotnet restore
# COPY movie-voting-back-end/ ./
# RUN dotnet publish -c Release -o out

# # Runtime stage
# FROM mcr.microsoft.com/dotnet/sdk:7.0 AS runtime
# WORKDIR /app

# # Copy the frontend build output from build-frontend stage to runtime stage
# COPY --from=build-frontend /app/movie-voting-front-end ./movie-voting-front-end
# EXPOSE 3000

# # Copy the backend build output from build-backend stage to runtime stage
# COPY --from=build-backend /app/movie-voting-back-end/out ./movie-voting-back-end
# EXPOSE 5005

# # Set entrypoint to start both frontend and backend
# ENTRYPOINT ["sh", "-c", "cd movie-voting-front-end && npm run dev & cd /app/movie-voting-back-end && dotnet movie-voting-back-end.dll"]

# Frontend build stage
FROM node:18 AS build-frontend
WORKDIR /app/movie-voting-front-end
COPY movie-voting-front-end/package*.json ./
RUN npm install
COPY movie-voting-front-end/ ./
RUN npm run build

# Backend build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-backend
WORKDIR /app/movie-voting-back-end/
COPY movie-voting-back-end/*.csproj ./
RUN dotnet restore
COPY movie-voting-back-end/ ./
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS runtime
WORKDIR /app

# Copy the frontend build output from build-frontend stage to runtime stage
COPY --from=build-frontend /app/movie-voting-front-end ./movie-voting-front-end
EXPOSE 3000

# Install Node.js 18 in the runtime stage
RUN curl -fsSL https://deb.nodesource.com/setup_18.x | bash -
RUN apt-get install -y nodejs

# Copy the backend build output from build-backend stage to runtime stage
COPY --from=build-backend /app/movie-voting-back-end/out ./movie-voting-back-end
EXPOSE 5005

# Set entrypoint to start both frontend and backend
ENTRYPOINT ["sh", "-c", "cd /app/movie-voting-front-end && npm run dev & cd /app/movie-voting-back-end && dotnet movie-voting-back-end.dll"]
