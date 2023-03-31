FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine

HEALTHCHECK --start-period=30s CMD wget --quiet --tries=1 --spider http://localhost:5000 || exit 1

EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000 \
    ASPNETCORE_ENVIRONMENT=Production

WORKDIR /
COPY src/Devantler.DataMesh.DataProduct/Devantler.DataMesh.DataProduct.csproj /build/src/Devantler.DataMesh.DataProduct/Devantler.DataMesh.DataProduct.csproj
COPY src/Devantler.DataMesh.DataProduct.Generator/Devantler.DataMesh.DataProduct.Generator.csproj /build/src/Devantler.DataMesh.DataProduct.Generator/Devantler.DataMesh.DataProduct.Generator.csproj
COPY src/Devantler.DataMesh.DataProduct.Configuration/Devantler.DataMesh.DataProduct.Configuration.csproj /build/src/Devantler.DataMesh.DataProduct.Configuration/Devantler.DataMesh.DataProduct.Configuration.csproj
COPY src/Devantler.DataMesh.SchemaRegistryClient/Devantler.DataMesh.SchemaRegistryClient.csproj /build/src/Devantler.DataMesh.SchemaRegistryClient/Devantler.DataMesh.SchemaRegistryClient.csproj

RUN dotnet restore build/src/Devantler.DataMesh.DataProduct/Devantler.DataMesh.DataProduct.csproj

COPY . .

WORKDIR /app
CMD ["cp config.* /build/src/Devantler.DataMesh.DataProduct \
    && dotnet publish /build/src/Devantler.DataMesh.DataProduct/Devantler.DataMesh.DataProduct.csproj -c Release -o ./  \
    && rm -rf /build \
    && wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh \
    && chmod +x ./dotnet-install.sh \
    && ./dotnet-install.sh --channel 7.0 --runtime aspnetcore \
    && rm -rf dotnet-install.sh \
    && adduser -D -u 1000 -g 1000 appuser \
    && chown -R appuser:appuser ./" ]

ENTRYPOINT [ "dotnet", "Devantler.DataMesh.DataProduct.dll" ]