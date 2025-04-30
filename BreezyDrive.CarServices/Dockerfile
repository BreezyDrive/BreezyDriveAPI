FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8280
EXPOSE 8281

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BreezyDrive.CarServices/BreezyDrive.CarServices.csproj", "BreezyDrive.CarServices/"]
COPY ["BreezyDrive.CommonService/BreezyDrive.CommonService.csproj", "BreezyDrive.CommonService/"]
COPY ["Library/Library.csproj", "Library/"]
RUN dotnet restore "BreezyDrive.CarServices/BreezyDrive.CarServices.csproj"
COPY . .
WORKDIR "/src/BreezyDrive.CarServices"
RUN dotnet build "./BreezyDrive.CarServices.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BreezyDrive.CarServices.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BreezyDrive.CarServices.dll"]
