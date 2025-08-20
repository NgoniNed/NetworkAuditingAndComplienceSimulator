FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

# Create a working directory within the container
WORKDIR /app

# Copy the .csproj file to the working directory
COPY CentralServer.csproj ./

# Restore the project dependencies
RUN dotnet restore CentralServer.csproj

# Copy the rest of the source code to the working directory
COPY ./ ./

# Build the application
RUN dotnet build CentralServer.csproj -c Release -o out

# Publish the application
RUN dotnet publish CentralServer.csproj -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy the published output from the build stage to the runtime image
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "CentralServer.dll"]
