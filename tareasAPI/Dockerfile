# Etapa 1: Construcción de la aplicación (Build)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el archivo .csproj y restaurar las dependencias
COPY *.sln ./
COPY [/*.csproj, tareasAPI/]
RUN dotnet restore

# Copiar todo el resto del proyecto y construirlo
COPY . .
WORKDIR /src/tareasAPI
RUN dotnet build -c Release -o /app/build

# Publicar la aplicación para la etapa de ejecución
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Imagen de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Exponer el puerto donde se ejecutará la aplicación
EXPOSE 80

# Ejecutar la aplicación
ENTRYPOINT ["dotnet", "tareasAPI.dll"]