namespace Devantler.DataMesh.DataProduct.Apis.REST;

public static class RESTStartupExtensions
{
    public static IServiceCollection AddRESTApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();
        return services;
    }
}
