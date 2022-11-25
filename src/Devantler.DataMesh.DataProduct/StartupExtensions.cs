using Microsoft.FeatureManagement;

namespace Devantler.DataMesh.DataProduct;

public static class StartupExtensions
{
    public static IServiceCollection AddFeatures(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFeatureManagement();
        //services.AddEntityFramework(configuration);
        //services.AddGraphQL(configuration);
        //services.AddStateStore(configuration);

        //services.AddControllers();
        //services.AddSwaggerGen();
        //services.AddEndpointsApiExplorer();

        return services;
    }

    public static void UseFeatures(this WebApplication app)
    {
        //app.UseGraphQL();
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
