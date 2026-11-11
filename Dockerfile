# ---------- Étape 1 : build ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copier d'abord le .csproj pour profiter du cache Docker sur la restauration des packages
COPY ProgressReport.csproj ./
RUN dotnet restore ProgressReport.csproj

# Copier le reste du code et publier
COPY . .
RUN dotnet publish ProgressReport.csproj -c Release -o /app/publish --no-restore

# ---------- Étape 2 : runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Exécuter en utilisateur non-root (fourni par l'image .NET 9)
USER $APP_UID

ENTRYPOINT ["dotnet", "ProgressReport.dll"]