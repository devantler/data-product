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
COPY ["src/Devantler.DataMesh.Provisioning/Devantler.DataMesh.Provisioning.csproj", "src/Devantler.DataMesh.Provisioning/"]
RUN dotnet restore "src/Devantler.DataMesh.Provisioning/Devantler.DataMesh.Provisioning.csproj"
COPY . .
WORKDIR "/src/src/Devantler.DataMesh.Provisioning"
RUN dotnet build "Devantler.DataMesh.Provisioning.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Devantler.DataMesh.Provisioning.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Devantler.DataMesh.Provisioning.dll"]
