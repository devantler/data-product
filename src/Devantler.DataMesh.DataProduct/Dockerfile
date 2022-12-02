FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /build

COPY Devantler.DataMesh.sln .
COPY src/Devantler.DataMesh.DataProduct.Web ./src/Devantler.DataMesh.DataProduct.Web
COPY src/Devantler.DataMesh.DataProduct.Web.SourceGenerator ./src/Devantler.DataMesh.DataProduct.Web.SourceGenerator
COPY src/Devantler.DataMesh.DataProduct.Core ./src/Devantler.DataMesh.DataProduct.Core
COPY src/Devantler.DataMesh.DataProduct.Core.SourceGenerator ./src/Devantler.DataMesh.DataProduct.Core.SourceGenerator
COPY src/Devantler.DataMesh.DataProduct.Configuration ./src/Devantler.DataMesh.DataProduct.Configuration
COPY packages/Devantler.SourceGenerator.Core ./packages/Devantler.SourceGenerator.Core

WORKDIR /build/src/Devantler.DataMesh.DataProduct.Web
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "Devantler.DataMesh.DataProduct.dll"]
