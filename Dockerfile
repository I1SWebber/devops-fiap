
# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY EnergyAPI/*.csproj ./EnergyAPI/
RUN dotnet restore ./EnergyAPI/EnergyAPI.csproj

COPY EnergyAPI ./EnergyAPI
WORKDIR /app/EnergyAPI
RUN dotnet publish ./EnergyAPI.csproj -c Release -o /out

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "EnergyAPI.dll"]
