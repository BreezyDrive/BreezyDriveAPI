FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BreezyDrive.Cars/BreezyDrive.Cars.csproj", "BreezyDrive.Cars/"]
RUN dotnet restore "BreezyDrive.Cars/BreezyDrive.Cars.csproj"
COPY . .
WORKDIR "/src/BreezyDrive.Cars"
RUN dotnet build "BreezyDrive.Cars.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BreezyDrive.Cars.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BreezyDrive.Cars.dll"]
