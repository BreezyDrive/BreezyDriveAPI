FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8180
EXPOSE 8181

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BreezyDrive.UserServices/BreezyDrive.UserServices.csproj", "BreezyDrive.UserServices/"]
COPY ["BreezyDrive.CommonService/BreezyDrive.CommonService.csproj", "BreezyDrive.CommonService/"]
COPY ["BreezyDrive.CommonService/shared.appsettings.Production.json", "BreezyDrive.CommonService/"]
COPY ["Library/Library.csproj", "Library/"]
RUN dotnet restore "BreezyDrive.UserServices/BreezyDrive.UserServices.csproj"
COPY . .
WORKDIR "/src/BreezyDrive.UserServices"
RUN dotnet build "./BreezyDrive.UserServices.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BreezyDrive.UserServices.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["BreezyDrive.CommonService/shared.appsettings.Production.json", "."]
ENTRYPOINT ["dotnet", "BreezyDrive.UserServices.dll"]
