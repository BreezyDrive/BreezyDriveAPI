FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BreezyDrive.PaymentServices/BreezyDrive.PaymentServices.csproj", "BreezyDrive.PaymentServices/"]
RUN dotnet restore "BreezyDrive.PaymentServices/BreezyDrive.PaymentServices.csproj"
COPY . .
WORKDIR "/src/BreezyDrive.PaymentServices"
RUN dotnet build "./BreezyDrive.PaymentServices.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BreezyDrive.PaymentServices.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BreezyDrive.PaymentServices.dll"]
