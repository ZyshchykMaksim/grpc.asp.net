FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/GamingHub.IdentityServer.API/GamingHub.IdentityServer.API.csproj", "services/GamingHub.IdentityServer.API/"]
RUN dotnet restore "services/GamingHub.IdentityServer.API/GamingHub.IdentityServer.API.csproj"
COPY . .
WORKDIR "/src/services/GamingHub.IdentityServer.API"
RUN dotnet build "GamingHub.IdentityServer.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GamingHub.IdentityServer.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GamingHub.IdentityServer.API.dll"]
