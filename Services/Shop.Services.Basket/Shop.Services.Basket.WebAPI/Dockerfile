FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Shop.Services.Basket/Shop.Services.Basket.WebAPI/Shop.Services.Basket.WebAPI.csproj", "Services/Shop.Services.Basket/Shop.Services.Basket.WebAPI/"]
COPY ["BuildingBlocks/Common.Logging/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Common.Logging/Common.Logging/"]
COPY ["Services/Shop.Services.Basket/Shop.Services.Basket.BusinessLogic/Shop.Services.Basket.BusinessLogic.csproj", "Services/Shop.Services.Basket/Shop.Services.Basket.BusinessLogic/"]
COPY ["Services/Shop.Services.Basket/Shop.Services.Basket.DataAccess/Shop.Services.Basket.DataAccess.csproj", "Services/Shop.Services.Basket/Shop.Services.Basket.DataAccess/"]
RUN dotnet restore "Services/Shop.Services.Basket/Shop.Services.Basket.WebAPI/Shop.Services.Basket.WebAPI.csproj"
COPY . .
WORKDIR "/src/Services/Shop.Services.Basket/Shop.Services.Basket.WebAPI"
RUN dotnet build "Shop.Services.Basket.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Shop.Services.Basket.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.Services.Basket.WebAPI.dll"]
