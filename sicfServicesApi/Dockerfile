#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["sicfServicesApi/sicfServicesApi.csproj", "sicfServicesApi/"]
COPY ["sicfExceptions/sicfExceptions.csproj", "sicfExceptions/"]
COPY ["sicf_Models/sicf_Models.csproj", "sicf_Models/"]
COPY ["sicf_Security/sicf_Security.csproj", "sicf_Security/"]
COPY ["sicf_BusinessHandlers/sicf_BusinessHandlers.csproj", "sicf_BusinessHandlers/"]
COPY ["sicf_DataBase/sicf_DataBase.csproj", "sicf_DataBase/"]
RUN dotnet restore "./sicfServicesApi/sicfServicesApi.csproj"
COPY . .
WORKDIR "/src/sicfServicesApi"
RUN dotnet build "./sicfServicesApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./sicfServicesApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sicfServicesApi.dll"]