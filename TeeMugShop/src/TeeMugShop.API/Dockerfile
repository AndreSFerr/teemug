FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY TeeMugShop.sln .
COPY TeeMugShop.API/TeeMugShop.API.csproj TeeMugShop.API/
COPY TeeMugShop.Application/TeeMugShop.Application.csproj TeeMugShop.Application/
COPY TeeMugShop.Domain/TeeMugShop.Domain.csproj TeeMugShop.Domain/
COPY TeeMugShop.Infrastructure/TeeMugShop.Infrastructure.csproj TeeMugShop.Infrastructure/

RUN dotnet restore

COPY . .
WORKDIR /src/TeeMugShop.API
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# ✅ Expõe porta padrão do ASP.NET
EXPOSE 80

# ✅ Garante que o ASP.NET escute externamente
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "TeeMugShop.API.dll"]
