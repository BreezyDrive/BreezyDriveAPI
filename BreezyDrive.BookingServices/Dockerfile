FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8580
EXPOSE 8581

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BreezyDrive.BookingServices/BreezyDrive.BookingServices.csproj", "BreezyDrive.BookingServices/"]
RUN dotnet restore "BreezyDrive.BookingServices/BreezyDrive.BookingServices.csproj"
COPY . .
WORKDIR "/src/BreezyDrive.BookingServices"
RUN dotnet build "./BreezyDrive.BookingServices.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BreezyDrive.BookingServices.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BreezyDrive.BookingServices.dll"]
