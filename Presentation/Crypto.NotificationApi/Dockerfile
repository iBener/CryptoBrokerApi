#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Presentation/Crypto.NotificationApi/Crypto.NotificationApi.csproj", "Presentation/Crypto.NotificationApi/"]
RUN dotnet restore "Presentation/Crypto.NotificationApi/Crypto.NotificationApi.csproj"
COPY . .
WORKDIR "/src/Presentation/Crypto.NotificationApi"
RUN dotnet build "Crypto.NotificationApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Crypto.NotificationApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Crypto.NotificationApi.dll"]