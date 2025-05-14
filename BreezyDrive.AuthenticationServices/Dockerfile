FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BreezyDrive.AuthenticationServices/BreezyDrive.AuthenticationServices.csproj", "BreezyDrive.AuthenticationServices/"]
COPY ["BreezyDrive.CommonService/BreezyDrive.CommonService.csproj", "BreezyDrive.CommonService/"]
COPY ["BreezyDrive.CommonService/shared.appsettings.Production.json", "BreezyDrive.CommonService/"]
COPY ["Library/Library.csproj", "Library/"]
RUN dotnet restore "BreezyDrive.AuthenticationServices/BreezyDrive.AuthenticationServices.csproj"
COPY . .
WORKDIR "/src/BreezyDrive.AuthenticationServices"
RUN dotnet build "./BreezyDrive.AuthenticationServices.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BreezyDrive.AuthenticationServices.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["BreezyDrive.CommonService/shared.appsettings.Production.json", "."]
ENTRYPOINT ["dotnet", "BreezyDrive.AuthenticationServices.dll"]
