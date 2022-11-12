FROM mcr.microsoft.com/dotnet/sdk:6.0
ENV ASPNETCORE_URLS=http://+:5001
EXPOSE 5001

WORKDIR /src

COPY Devantler.DataMesh.DataProduct ./Devantler.DataMesh.DataProduct
COPY Devantler.DataMesh.DataProduct.SourceGenerators ./Devantler.DataMesh.DataProduct.SourceGenerators

RUN dotnet dev-certs https --trust
WORKDIR /src/Devantler.DataMesh.DataProduct
ENTRYPOINT ["dotnet", "run", "-c Release"]
