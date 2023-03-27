FROM mcr.microsoft.com/dotnet/sdk:7.0 AS app
WORKDIR /app

EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production

COPY Devantler.DataMesh.sln .
COPY src/Devantler.DataMesh.DataProduct/Devantler.DataMesh.DataProduct.csproj ./src/Devantler.DataMesh.DataProduct/
COPY src/Devantler.DataMesh.DataProduct.Generator/Devantler.DataMesh.DataProduct.Generator.csproj ./src/Devantler.DataMesh.DataProduct.Generator/
COPY src/Devantler.DataMesh.DataProduct.Configuration/Devantler.DataMesh.DataProduct.Configuration.csproj ./src/Devantler.DataMesh.DataProduct.Configuration/
COPY src/Devantler.DataMesh.SchemaRegistryClient/Devantler.DataMesh.SchemaRegistryClient.csproj ./src/Devantler.DataMesh.SchemaRegistryClient/

RUN dotnet restore src/Devantler.DataMesh.DataProduct/Devantler.DataMesh.DataProduct.csproj

COPY . .

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
# RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
# USER appuser

WORKDIR /app/src/Devantler.DataMesh.DataProduct

ENTRYPOINT ["dotnet", "run", "-c Release", "--no-launch-profile"]
