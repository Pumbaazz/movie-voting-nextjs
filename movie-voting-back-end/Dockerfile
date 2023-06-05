# FROM mcr.microsoft.com/dotnet/core/aspnet:7.0-alpine as base
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS BUILD
WORKDIR /app

ENV ASPNETCORE_URLS="http://+:5005"
# EXPOSE 80

COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet build --configuration Release
#  --no-restore

# Publish the project
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS RUNTIME
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 5005

# Add the following lines to run migrations during container startup
# CMD ["dotnet", "ef", "database", "update"]

# Entry point for executing the application
ENTRYPOINT ["dotnet", "movie-voting-back-end.dll", "--server.urls", "http://+:5005"]