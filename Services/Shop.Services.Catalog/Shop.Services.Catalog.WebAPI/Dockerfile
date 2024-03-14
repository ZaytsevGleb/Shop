FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Shop.Services.Catalog/Shop.Services.Catalog.WebAPI/Shop.Services.Catalog.WebAPI.csproj", "Services/Shop.Services.Catalog/Shop.Services.Catalog.WebAPI/"]
COPY ["Services/Shop.Services.Catalog/Shop.Services.Catalog.BusinessLogic/Shop.Services.Catalog.BusinessLogic.csproj", "Services/Shop.Services.Catalog/Shop.Services.Catalog.BusinessLogic/"]
COPY ["Services/Shop.Services.Catalog/Shop.Services.Catalog.DataAccess/Shop.Services.Catalog.DataAccess.csproj", "Services/Shop.Services.Catalog/Shop.Services.Catalog.DataAccess/"]
COPY ["Services/Shop.Services.Catalog/Shop.Services.Catalog.Shared/Shop.Services.Catalog.Shared.csproj", "Services/Shop.Services.Catalog/Shop.Services.Catalog.Shared/"]
COPY ["BuildingBlocks/Common.Logging/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Common.Logging/Common.Logging/"]
RUN dotnet restore "Services/Shop.Services.Catalog/Shop.Services.Catalog.WebAPI/Shop.Services.Catalog.WebAPI.csproj"
COPY . .
WORKDIR "/src/Services/Shop.Services.Catalog/Shop.Services.Catalog.WebAPI"
RUN dotnet build "Shop.Services.Catalog.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Shop.Services.Catalog.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.Services.Catalog.WebAPI.dll"]
