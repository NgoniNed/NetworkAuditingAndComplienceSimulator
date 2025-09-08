FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /app

COPY CentralServer.csproj ./

RUN dotnet restore CentralServer.csproj

COPY ./ ./

RUN dotnet build CentralServer.csproj -c Release -o out

RUN dotnet publish CentralServer.csproj -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "CentralServer.dll"]
