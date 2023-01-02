ARG GITHUB_TOKEN

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /build

COPY Devantler.DataMesh.sln .
COPY src/Devantler.DataMesh.DataProduct/Devantler.DataMesh.DataProduct.csproj ./src/Devantler.DataMesh.DataProduct/
COPY src/Devantler.DataMesh.DataProduct.SourceGenerator/Devantler.DataMesh.DataProduct.SourceGenerator.csproj ./src/Devantler.DataMesh.DataProduct.SourceGenerator/
COPY src/Devantler.DataMesh.DataProduct.Configuration/Devantler.DataMesh.DataProduct.Configuration.csproj ./src/Devantler.DataMesh.DataProduct.Configuration/
COPY src/Devantler.DataMesh.SchemaRegistry/Devantler.DataMesh.SchemaRegistry.csproj ./src/Devantler.DataMesh.SchemaRegistry/

RUN dotnet nuget add source --username devantler --password ${GITHUB_TOKEN} --store-password-in-clear-text --name github "https://nuget.pkg.github.com/devantler/index.json"
RUN dotnet restore src/Devantler.DataMesh.DataProduct/Devantler.DataMesh.DataProduct.csproj

COPY . .

WORKDIR /build/src/Devantler.DataMesh.DataProduct
RUN dotnet build "Devantler.DataMesh.DataProduct.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Devantler.DataMesh.DataProduct.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Devantler.DataMesh.DataProduct.dll"]
