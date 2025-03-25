FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Caminhos corrigidos porque o contexto é a raiz, mas este Dockerfile está dentro de src/TeeMugShop.API
COPY TeeMugShop.sln ../
COPY src/TeeMugShop.API/TeeMugShop.API.csproj ../src/TeeMugShop.API/
COPY src/TeeMugShop.Application/TeeMugShop.Application.csproj ../src/TeeMugShop.Application/
COPY src/TeeMugShop.Domain/TeeMugShop.Domain.csproj ../src/TeeMugShop.Domain/
COPY src/TeeMugShop.Infrastructure/TeeMugShop.Infrastructure.csproj ../src/TeeMugShop.Infrastructure/

WORKDIR /src
RUN dotnet restore ../TeeMugShop.sln

COPY ../ .
WORKDIR /src/src/TeeMugShop.API
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "TeeMugShop.API.dll"]
