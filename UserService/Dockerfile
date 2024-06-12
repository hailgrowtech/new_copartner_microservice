# Use the official ASP.NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the official ASP.NET Core SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY ["UserService/UserService.csproj", "UserService/"]
COPY ["CommonLibrary/CommonLibrary.csproj", "CommonLibrary/"]
COPY ["MigrationDB/MigrationDB.csproj", "MigrationDB/"]

# Restore dependencies
RUN dotnet restore "./UserService/UserService.csproj"

# Copy the rest of the project files
COPY . .

WORKDIR "/src/UserService"

# Build the project
RUN dotnet build "./UserService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the project
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UserService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Create the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Optionally, add a non-root user and use it for better security
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

ENTRYPOINT ["dotnet", "UserService.dll"]
