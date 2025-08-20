#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /app

COPY HumanResource.csproj ./

RUN dotnet restore HumanResource.csproj

COPY ./ ./

RUN dotnet build HumanResource.csproj -c Release -o out

RUN dotnet publish HumanResource.csproj -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "HumanResource.dll"]
