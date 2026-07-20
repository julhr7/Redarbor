# 1. Etapa de Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# 2. Etapa de Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Redarbor.Api/Redarbor.Api.csproj", "Redarbor.Api/"]
COPY ["Redarbor.Application/Redarbor.Application.csproj", "Redarbor.Application/"]
COPY ["Redarbor.Domain/Redarbor.Domain.csproj", "Redarbor.Domain/"]
COPY ["Redarbor.Infrastructure/Redarbor.Infrastructure.csproj", "Redarbor.Infrastructure/"]
RUN dotnet restore "Redarbor.Api/Redarbor.Api.csproj"

COPY . .
WORKDIR "/src/Redarbor.Api"
RUN dotnet build "Redarbor.Api.csproj" -c Release -o /app/build

# 3. Etapa de Publish
FROM build AS publish
RUN dotnet publish "Redarbor.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 4. Etapa Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Redarbor.Api.dll"]