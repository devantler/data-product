using Devantler.DataMesh.DataProduct.Clients.DatabaseClients;

namespace Devantler.DataMesh.DataProduct.Clients;

public static class ClientsStartupExtensions
{
    public static void AddClients(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDatabaseClient(configuration);
}
