# # BUILD PHASE
# FROM mcr.microsoft.com/dotnet/sdk:7.0 AS BUILD-BACKEND
# WORKDIR /app/movie-voting-back-end
# COPY movie-voting-back-end/*.csproj ./
# RUN dotnet restore
# COPY movie-voting-back-end/ .
# RUN dotnet build -c Release -o out

# FROM node:18-alpine as BUILD-FRONTEND
# WORKDIR /app/movie-voting-front-end
# COPY movie-voting-front-end/package*.json ./
# RUN npm install
# COPY movie-voting-front-end/ .
# # RUN npm run build
# CMD [ "sh", "-c", "cd", "/app/movie-voting-front-end", "npm", "run", "build" ]


# # RUN rm -rf .env
# # CMD ["npm", "run", "dev"]

# # RUNTIME PHASE
# FROM mcr.microsoft.com/dotnet/sdk:7.0 AS RUNTIME
# WORKDIR /app
# COPY --from=BUILD-BACKEND /app/movie-voting-back-end/out .
# COPY --from=BUILD-FRONTEND /app/movie-voting-front-end/.next ./.next
# EXPOSE 3000
# EXPOSE 5005
# WORKDIR /app/movie-voting-front-end
# CMD [ "npm", "run", "dev" ]
# WORKDIR /app
# # Entry point for executing the application
# ENTRYPOINT ["dotnet", "movie-voting-back-end.dll"]

# Frontend build stage
FROM node:18-alpine AS build-frontend
WORKDIR /app/movie-voting-front-end
COPY movie-voting-front-end/package*.json ./
RUN npm install
COPY movie-voting-front-end/ .
CMD [ "sh", "-c", "cd", "/app/movie-voting-front-end", "npm", "run", "build" ]

# Backend build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-backend
WORKDIR /app/movie-voting-back-end
COPY movie-voting-back-end/*.csproj ./
RUN dotnet restore
COPY movie-voting-back-end/ .
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS runtime
FROM node:18-alpine
WORKDIR /app

# Copy the frontend build output from build-frontend stage to runtime stage
COPY --from=build-frontend /app/movie-voting-front-end/ .

# Copy the backend build output from build-backend stage to runtime stage
COPY --from=build-backend /app/movie-voting-back-end/out .

# Set the entrypoint to run the backend application
# ENTRYPOINT ["dotnet", "movie-voting-back-end.dll", "&", "sh", "-c", "cd", "/app/movie-voting-front-end", "npm", "run", "dev"]
ENTRYPOINT ["npm run dev & dotnet movie-voting-back-end.dll"]
