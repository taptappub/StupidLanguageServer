# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /appSource

COPY . ./
RUN ls -la /
RUN dotnet restore

RUN dotnet build -c Release

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /appSource/src/WebAPI/bin/Release/net6.0/ .
ENTRYPOINT ["dotnet", "WebAPI.dll"]
EXPOSE 80