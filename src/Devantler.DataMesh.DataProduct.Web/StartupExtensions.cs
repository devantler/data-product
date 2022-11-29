using Microsoft.FeatureManagement;
using Devantler.DataMesh.DataProduct.Web.Features.Apis;
using Devantler.DataMesh.DataProduct.Web.Features.Clients;

namespace Devantler.DataMesh.DataProduct.Web;

public static class StartupExtensions
{
    public static IServiceCollection AddFeatures(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFeatureManagement();
        services.AddApis(configuration);
        services.AddClients(configuration);

        return services;
    }

    public static void UseFeatures(this WebApplication app, IConfiguration configuration)
    {
        // app.UseApis();
        //app.UseGraphQL(configuration);
        // Configure the HTTP request pipeline.
        // if (app.Environment.IsDevelopment())
        // {
        //     app.UseSwagger();
        //     app.UseSwaggerUI();
        // }
        // app.UseAuthorization();
        //app.MapControllers();
    }
}
