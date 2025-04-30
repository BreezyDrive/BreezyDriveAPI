FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["APIGateway/APIGateway.csproj", "APIGateway/"]
COPY ["BreezyDrive.BookingServices/BreezyDrive.BookingServices.csproj", "BreezyDrive.BookingServices/"]
COPY ["BreezyDrive.CarServices/BreezyDrive.CarServices.csproj", "BreezyDrive.CarServices/"]
COPY ["BreezyDrive.CommonService/BreezyDrive.CommonService.csproj", "BreezyDrive.CommonService/"]
COPY ["Library/Library.csproj", "Library/"]
COPY ["BreezyDrive.ConversationServices/BreezyDrive.ConversationServices.csproj", "BreezyDrive.ConversationServices/"]
COPY ["BreezyDrive.NotificationServices/BreezyDrive.NotificationServices.csproj", "BreezyDrive.NotificationServices/"]
COPY ["BreezyDrive.UserServices/BreezyDrive.UserServices.csproj", "BreezyDrive.UserServices/"]
RUN dotnet restore "APIGateway/APIGateway.csproj"
COPY . .
WORKDIR "/src/APIGateway"
RUN dotnet build "./APIGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./APIGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIGateway.dll"]
