#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Stock/EStk.API/EStk.API.csproj", "Services/Stock/EStk.API/"]
COPY ["Services/Stock/Core/Estk.Core/Estk.Core.csproj", "Services/Stock/Core/Estk.Core/"]
COPY ["Services/Stock/Core/Estk.Core.Domain/Estk.Core.Domain.csproj", "Services/Stock/Core/Estk.Core.Domain/"]
COPY ["Services/Stock/Infrastructure/Estk.Infrastructure/Estk.Infrastructure.csproj", "Services/Stock/Infrastructure/Estk.Infrastructure/"]
RUN dotnet restore "Services/Stock/EStk.API/EStk.API.csproj"
COPY . .
WORKDIR "/src/Services/Stock/EStk.API"
RUN dotnet build "EStk.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EStk.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EStk.API.dll"]