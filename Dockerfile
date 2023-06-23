# Frontend build stage
FROM node:18 AS build-frontend
# RUN npm install --global yarn
WORKDIR /movie-voting-front-end
COPY movie-voting-front-end/package*.json ./
# COPY movie-voting-front-end/*.lock ./
RUN npm install
# RUN yarn install
COPY movie-voting-front-end/ ./
RUN npm run build
# RUN yarn build

# Backend build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-backend
WORKDIR /movie-voting-back-end/
COPY movie-voting-back-end/*.csproj ./
RUN dotnet restore
COPY movie-voting-back-end/ ./
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS runtime
WORKDIR /

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_18.x | bash -
RUN apt-get install -y nodejs
RUN npm install --global yarn

# Copy the build output to runtime stage
COPY --from=build-frontend /movie-voting-front-end ./movie-voting-front-end
EXPOSE 3000

COPY --from=build-backend /movie-voting-back-end/out ./movie-voting-back-end
EXPOSE 5005

# Set entrypoint to start both frontend and backend
ENTRYPOINT ["sh", "-c", "cd /movie-voting-front-end && npm run dev & cd /movie-voting-back-end && dotnet movie-voting-back-end.dll"]
# ENTRYPOINT ["sh", "-c", "cd /movie-voting-front-end && yarn dev & cd /movie-voting-back-end && dotnet movie-voting-back-end.dll"]
