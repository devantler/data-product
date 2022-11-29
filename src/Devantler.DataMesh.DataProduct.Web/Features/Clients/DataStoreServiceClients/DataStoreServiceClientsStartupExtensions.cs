namespace Devantler.DataMesh.DataProduct.Web.Features.Clients.DataStoreServiceClients;

public static partial class DataStoreServiceClientsStartupExtensions
{
    public static IServiceCollection AddDataStoreServiceClient(this IServiceCollection services, IConfiguration configuration)
    {
        AddDataStoreServiceClientFromSourceOutput(services, configuration);
        return services;
    }

    static partial void AddDataStoreServiceClientFromSourceOutput(IServiceCollection services, IConfiguration configuration);
}
