FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BreezyDrive.Bookings/BreezyDrive.Bookings.csproj", "BreezyDrive.Bookings/"]
RUN dotnet restore "BreezyDrive.Bookings/BreezyDrive.Bookings.csproj"
COPY . .
WORKDIR "/src/BreezyDrive.Bookings"
RUN dotnet build "BreezyDrive.Bookings.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BreezyDrive.Bookings.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BreezyDrive.Bookings.dll"]
