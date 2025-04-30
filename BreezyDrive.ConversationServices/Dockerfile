FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8380
EXPOSE 8381

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BreezyDrive.ConversationServices/BreezyDrive.ConversationServices.csproj", "BreezyDrive.ConversationServices/"]
COPY ["BreezyDrive.CommonService/BreezyDrive.CommonService.csproj", "BreezyDrive.CommonService/"]
COPY ["Library/Library.csproj", "Library/"]
COPY ["BreezyDrive.NotificationServices/BreezyDrive.NotificationServices.csproj", "BreezyDrive.NotificationServices/"]
COPY ["BreezyDrive.UserServices/BreezyDrive.UserServices.csproj", "BreezyDrive.UserServices/"]
RUN dotnet restore "BreezyDrive.ConversationServices/BreezyDrive.ConversationServices.csproj"
COPY . .
WORKDIR "/src/BreezyDrive.ConversationServices"
RUN dotnet build "./BreezyDrive.ConversationServices.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BreezyDrive.ConversationServices.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BreezyDrive.ConversationServices.dll"]
