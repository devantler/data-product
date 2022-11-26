using Microsoft.FeatureManagement;
using Devantler.DataMesh.DataProduct.Apis;
namespace Devantler.DataMesh.DataProduct;

public static class StartupExtensions
{
    public static IServiceCollection AddFeatures(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFeatureManagement();
        services.AddApis(configuration);
        //services.AddGraphQL(configuration);
        //services.AddEntityFramework(configuration);
        //services.AddGraphQL(configuration);
        //services.AddStateStore(configuration);

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
