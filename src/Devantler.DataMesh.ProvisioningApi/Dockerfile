FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 3000

ENV ASPNETCORE_URLS=http://+:3000

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Devantler.DataMesh.ProvisioningApi/Devantler.DataMesh.ProvisioningApi.csproj", "src/Devantler.DataMesh.ProvisioningApi/"]
RUN dotnet restore "src/Devantler.DataMesh.ProvisioningApi/Devantler.DataMesh.ProvisioningApi.csproj"
COPY . .
WORKDIR "/src/src/Devantler.DataMesh.ProvisioningApi"
RUN dotnet build "Devantler.DataMesh.ProvisioningApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Devantler.DataMesh.ProvisioningApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Devantler.DataMesh.ProvisioningApi.dll"]
