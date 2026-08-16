FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Csproj fájlok átmásolása a függőségek gyors cache-eléséhez
COPY ["src/Ecommerce.API/Ecommerce.API.csproj", "src/Ecommerce.API/"]
COPY ["src/Ecommerce.Shared/Ecommerce.Shared.csproj", "src/Ecommerce.Shared/"]

RUN dotnet restore "src/Ecommerce.API/Ecommerce.API.csproj"

# Teljes forráskód másolása és publish
COPY . .
RUN dotnet publish "src/Ecommerce.API/Ecommerce.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 2. RUNTIME STAGE
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Ecommerce.API.dll"]