FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8680
EXPOSE 8681

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BreezyDrive.NotificationServices/BreezyDrive.NotificationServices.csproj", "BreezyDrive.NotificationServices/"]
COPY ["BreezyDrive.CommonService/BreezyDrive.CommonService.csproj", "BreezyDrive.CommonService/"]
COPY ["Library/Library.csproj", "Library/"]
RUN dotnet restore "BreezyDrive.NotificationServices/BreezyDrive.NotificationServices.csproj"
COPY . .
WORKDIR "/src/BreezyDrive.NotificationServices"
RUN dotnet build "./BreezyDrive.NotificationServices.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BreezyDrive.NotificationServices.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BreezyDrive.NotificationServices.dll"]
