FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 60000

ENV ASPNETCORE_URLS=http://+:60000

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY Devantler.DataMesh.Services.Dataspace/*.csproj Devantler.DataMesh.Services.Dataspace
COPY Devantler.DataMesh.Services.Core/*.csproj Devantler.DataMesh.Services.Core
RUN dotnet restore Devantler.DataMesh.Services.Dataspace/Devantler.DataMesh.Services.Dataspace.csproj
COPY Devantler.DataMesh.Services.Dataspace .
COPY Devantler.DataMesh.Services.Core .
RUN dotnet build Devantler.DataMesh.Services.Dataspace/Devantler.DataMesh.Services.Dataspace.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Devantler.DataMesh.Services.Dataspace/Devantler.DataMesh.Services.Dataspace.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Devantler.DataMesh.Services.Dataspace.dll"]
