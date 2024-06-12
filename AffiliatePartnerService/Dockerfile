#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5197
EXPOSE 5133

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AffiliatePartnerService/AffiliatePartnerService.csproj", "AffiliatePartnerService/"]
COPY ["CommonLibrary/CommonLibrary.csproj", "CommonLibrary/"]
COPY ["MigrationDB/MigrationDB.csproj", "MigrationDB/"]
RUN dotnet restore "./AffiliatePartnerService/AffiliatePartnerService.csproj"
COPY . .
WORKDIR "/src/AffiliatePartnerService"
RUN dotnet build "./AffiliatePartnerService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AffiliatePartnerService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "AffiliatePartnerService.dll"]