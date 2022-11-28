namespace Devantler.DataMesh.DataProduct.Clients.DatabaseClients;

public static partial class DatabaseClientsStartupExtensions
{
    public static IServiceCollection AddDatabaseClient(this IServiceCollection services, IConfiguration configuration)
    {
        AddDatabaseClientFromSourceOutput(services, configuration);
        return services;
    }

    static partial void AddDatabaseClientFromSourceOutput(IServiceCollection services, IConfiguration configuration);
}
