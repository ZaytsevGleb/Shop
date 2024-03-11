FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Cryptex.Services.OperationService.WebAPI/Cryptex.Services.OperationService.WebAPI.csproj", "Cryptex.Services.OperationService.WebAPI/"]
RUN dotnet restore "Cryptex.Services.OperationService.WebAPI/Cryptex.Services.OperationService.WebAPI.csproj"
COPY . .
WORKDIR "/src/Cryptex.Services.OperationService.WebAPI"
RUN dotnet build "Cryptex.Services.OperationService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Cryptex.Services.OperationService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cryptex.Services.OperationService.WebAPI.dll"]
