#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
#ENV DB_CONNECTION_STRING="Server=sqlserverdocker;Database=TogoDBDocker;User Id=sa;Password=Xcontra1256$;TrustServerCertificate=true"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["XDev_AIO/XDev_AIO.csproj", "XDev_AIO/"]
COPY ["XDev_TogoApi/XDev_TogoApi.csproj", "XDev_TogoApi/"]
COPY ["XDev_Model/XDev_Model.csproj", "XDev_Model/"]
COPY ["XDev_UnitWork/XDev_UnitWork.csproj", "XDev_UnitWork/"]
RUN dotnet restore "./XDev_TogoApi/XDev_TogoApi.csproj"
COPY . .
WORKDIR "/src/XDev_TogoApi"
RUN dotnet build "./XDev_TogoApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./XDev_TogoApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XDev_TogoApi.dll"]