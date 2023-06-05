# FROM mcr.microsoft.com/dotnet/core/aspnet:7.0-alpine as base
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS BUILD
WORKDIR /app

ENV ASPNETCORE_URLS="http://+:5005"
# EXPOSE 80

COPY *.csproj ./
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build

# Install the Entity Framework Core tools
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"
# Set the default environment variables for the container
ENV ASPNETCORE_ENVIRONMENT=Development \
    ConnectionStrings__DefaultConnection="Host=localhost;Database=Movie;Username=postgres;Password=postgres;"
RUN dotnet ef database update

# Publish the project
# RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS RUNTIME
WORKDIR /app
COPY --from=build /app/build ./
EXPOSE 5005

# Add the following lines to run migrations during container startup
# CMD ["dotnet", "ef", "database", "update"]

# Entry point for executing the application
ENTRYPOINT ["dotnet", "movie-voting-back-end.dll", "--server.urls", "http://+:5005"]