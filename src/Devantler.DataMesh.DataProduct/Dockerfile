FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /build

COPY Devantler.DataMesh.sln .
COPY src/Devantler.DataMesh.DataProduct/Devantler.DataMesh.DataProduct.csproj ./src/Devantler.DataMesh.DataProduct/
COPY src/Devantler.DataMesh.DataProduct.Generator/Devantler.DataMesh.DataProduct.Generator.csproj ./src/Devantler.DataMesh.DataProduct.Generator/
COPY src/Devantler.DataMesh.DataProduct.Configuration/Devantler.DataMesh.DataProduct.Configuration.csproj ./src/Devantler.DataMesh.DataProduct.Configuration/
COPY src/Devantler.DataMesh.SchemaRegistry/Devantler.DataMesh.SchemaRegistry.csproj ./src/Devantler.DataMesh.SchemaRegistry/

RUN dotnet restore src/Devantler.DataMesh.DataProduct/Devantler.DataMesh.DataProduct.csproj

COPY . .

WORKDIR /build/src/Devantler.DataMesh.DataProduct
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Devantler.DataMesh.DataProduct.dll"]
