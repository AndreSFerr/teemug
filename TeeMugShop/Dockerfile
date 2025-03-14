# Etapa 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar os arquivos de solução e restaurar dependências
COPY TeeMugShop.sln .
COPY TeeMugShop.API/TeeMugShop.API.csproj TeeMugShop.API/
COPY TeeMugShop.Application/TeeMugShop.Application.csproj TeeMugShop.Application/
COPY TeeMugShop.Domain/TeeMugShop.Domain.csproj TeeMugShop.Domain/
COPY TeeMugShop.Infrastructure/TeeMugShop.Infrastructure.csproj TeeMugShop.Infrastructure/

RUN dotnet restore

# Copiar tudo e compilar
COPY . .
WORKDIR /src/TeeMugShop.API
RUN dotnet publish -c Release -o /app/publish

# Etapa 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TeeMugShop.API.dll"]
