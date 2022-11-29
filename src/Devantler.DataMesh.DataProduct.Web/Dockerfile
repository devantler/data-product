FROM mcr.microsoft.com/dotnet/sdk:6.0
ENV ASPNETCORE_URLS=http://+:5001
EXPOSE 5001

WORKDIR /src

COPY Devantler.DataMesh.DataProduct ./Devantler.DataMesh.DataProduct
COPY Devantler.DataMesh.DataProduct.SourceGenerator ./Devantler.DataMesh.DataProduct.SourceGenerator
COPY Devantler.DataMesh.DataProduct.Core ./Devantler.DataMesh.DataProduct.Core
COPY Devantler.DataMesh.DataProduct.Core.SourceGenerator ./Devantler.DataMesh.DataProduct.Core.SourceGenerator

RUN dotnet dev-certs https --trust
WORKDIR /src/Devantler.DataMesh.DataProduct
ENTRYPOINT ["dotnet", "run", "-c Release"]
