FROM mcr.microsoft.com/dotnet/sdk:7.0

HEALTHCHECK --start-period=30s CMD wget --quiet --tries=1 --spider http://localhost:5000 || exit 1

EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000 \
    ASPNETCORE_ENVIRONMENT=Production

WORKDIR /build
COPY src/Devantler.DataMesh.DataProduct/Devantler.DataMesh.DataProduct.csproj ./src/Devantler.DataMesh.DataProduct/
COPY src/Devantler.DataMesh.DataProduct.Generator/Devantler.DataMesh.DataProduct.Generator.csproj ./src/Devantler.DataMesh.DataProduct.Generator/
COPY src/Devantler.DataMesh.DataProduct.Configuration/Devantler.DataMesh.DataProduct.Configuration.csproj ./src/Devantler.DataMesh.DataProduct.Configuration/
COPY src/Devantler.DataMesh.SchemaRegistryClient/Devantler.DataMesh.SchemaRegistryClient.csproj ./src/Devantler.DataMesh.SchemaRegistryClient/

RUN dotnet restore src/Devantler.DataMesh.DataProduct/Devantler.DataMesh.DataProduct.csproj

COPY . .

WORKDIR /
COPY src/Devantler.DataMesh.DataProduct/dockerfile-entrypoint.sh ./dockerfile-entrypoint.sh
RUN chmod +x ./dockerfile-entrypoint.sh
CMD ["./dockerfile-entrypoint.sh"]