# Etapa base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar e restaurar dependências
COPY ["src/TeeMugShop.API/TeeMugShop.API.csproj", "src/TeeMugShop.API/"]
COPY ["src/TeeMugShop.Application/TeeMugShop.Application.csproj", "src/TeeMugShop.Application/"]
COPY ["src/TeeMugShop.Domain/TeeMugShop.Domain.csproj", "src/TeeMugShop.Domain/"]
COPY ["src/TeeMugShop.Infrastructure/TeeMugShop.Infrastructure.csproj", "src/TeeMugShop.Infrastructure/"]

RUN dotnet restore "src/TeeMugShop.API/TeeMugShop.API.csproj" --verbosity detailed

# Copiar o restante do código e construir a aplicação
COPY . . 
WORKDIR "/src/TeeMugShop.API"
RUN dotnet build "TeeMugShop.API.csproj" -c Release -o /app/build

# Etapa de publicação
FROM build AS publish
RUN dotnet publish "TeeMugShop.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final para execução
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeeMugShop.API.dll"]
