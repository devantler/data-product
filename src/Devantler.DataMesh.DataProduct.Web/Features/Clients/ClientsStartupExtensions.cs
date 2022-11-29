using Devantler.DataMesh.DataProduct.Web.Features.Clients.DataStoreServiceClients;

namespace Devantler.DataMesh.DataProduct.Web.Features.Clients;

public static class ClientsStartupExtensions
{
    public static void AddClients(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDataStoreServiceClient(configuration);
}
