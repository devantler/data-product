FROM mcr.microsoft.com/dotnet/sdk:7.0

HEALTHCHECK --start-period=30s CMD wget --quiet --tries=1 --spider http://localhost:5000 || exit 1

EXPOSE 5000

ARG DOCKER_METADATA_OUTPUT_VERSION=v0.0.0
ENV ASPNETCORE_URLS=http://+:5000 \
    ASPNETCORE_ENVIRONMENT=Production \
    RELEASE=${DOCKER_METADATA_OUTPUT_VERSION}


WORKDIR /build
COPY src/Devantler.DataProduct/Devantler.DataProduct.csproj ./src/Devantler.DataProduct/
COPY src/Devantler.DataProduct.Generator/Devantler.DataProduct.Generator.csproj ./src/Devantler.DataProduct.Generator/
COPY src/Devantler.DataProduct.Core.Configuration/Devantler.DataProduct.Core.Configuration.csproj ./src/Devantler.DataProduct.Core.Configuration/
COPY src/Devantler.SchemaRegistry.Client/Devantler.SchemaRegistry.Client.csproj ./src/Devantler.SchemaRegistry.Client/

RUN dotnet restore src/Devantler.DataProduct/Devantler.DataProduct.csproj

COPY . .

WORKDIR /
COPY src/Devantler.DataProduct/dockerfile-entrypoint.sh ./dockerfile-entrypoint.sh
RUN chmod +x ./dockerfile-entrypoint.sh
CMD ["./dockerfile-entrypoint.sh"]