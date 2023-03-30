FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine

WORKDIR /build

EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000 \
    ASPNETCORE_ENVIRONMENT=Production

COPY src/Devantler.DataMesh.DataProduct/Devantler.DataMesh.DataProduct.csproj src/Devantler.DataMesh.DataProduct/Devantler.DataMesh.DataProduct.csproj
COPY src/Devantler.DataMesh.DataProduct.Generator/Devantler.DataMesh.DataProduct.Generator.csproj src/Devantler.DataMesh.DataProduct.Generator/Devantler.DataMesh.DataProduct.Generator.csproj
COPY src/Devantler.DataMesh.DataProduct.Configuration/Devantler.DataMesh.DataProduct.Configuration.csproj src/Devantler.DataMesh.DataProduct.Configuration/Devantler.DataMesh.DataProduct.Configuration.csproj
COPY src/Devantler.DataMesh.SchemaRegistryClient/Devantler.DataMesh.SchemaRegistryClient.csproj src/Devantler.DataMesh.SchemaRegistryClient/Devantler.DataMesh.SchemaRegistryClient.csproj

RUN dotnet restore src/Devantler.DataMesh.DataProduct/Devantler.DataMesh.DataProduct.csproj

COPY . .

WORKDIR /app
#Copy all config.{yaml,yml,json} files in /app to /build/src/Devantler.DataMesh.DataProduct with CMD
CMD cp config.* /build/src/Devantler.DataMesh.DataProduct \
    && dotnet publish -c Release -o ./ /build/src/Devantler.DataMesh.DataProduct/Devantler.DataMesh.DataProduct.csproj \
    && rm -rf /build \
    && adduser -D -u 1000 -g 1000 appuser \
    && chown -R appuser:appuser ./ \
    && dotnet /app/Devantler.DataMesh.DataProduct.dll