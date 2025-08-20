#putting on hold building this docker file for now
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Server/CyberSecurity.Server.csproj", "CyberSecurity/CyberSecurity/Server/"]
COPY ["Client/CyberSecurity.Client.csproj", "CyberSecurity/CyberSecurity/Client/"]
COPY ["Shared/CyberSecurity.Shared.csproj", "CyberSecurity/CyberSecurity/Shared/"]

RUN dotnet restore 'Server/CyberSecurity.Server.csproj'

COPY . .
WORKDIR "/src/CyberSecurity/CyberSecurity/Server"

RUN dotnet build "CyberSecurity.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CyberSecurity.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CyberSecurity.Server.dll"]
