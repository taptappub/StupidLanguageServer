 # syntax=docker/dockerfile:1
  FROM mcr.microsoft.com/dotnet/aspnet:6.0
  COPY src/WebAPI/bin/Release/net6.0/ App/
  WORKDIR /App
  ENTRYPOINT ["dotnet", "WebAPI.dll"]
  EXPOSE 80