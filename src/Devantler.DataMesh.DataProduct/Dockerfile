FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /build

COPY Devantler.DataMesh.sln .
COPY src/Devantler.DataMesh.DataProduct ./src/Devantler.DataMesh.DataProduct
COPY src/Devantler.DataMesh.DataProduct.SourceGenerator ./src/Devantler.DataMesh.DataProduct.SourceGenerator
COPY src/Devantler.DataMesh.DataProduct.Configuration ./src/Devantler.DataMesh.DataProduct.Configuration
COPY src/Devantler.DataMesh.SchemaRegistry ./src/Devantler.DataMesh.SchemaRegistry

WORKDIR /build/src/Devantler.DataMesh.DataProduct
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "Devantler.DataMesh.DataProduct.dll"]
