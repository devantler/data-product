using Devantler.DataMesh.DataProduct.Apis;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.DataStore;
namespace Devantler.DataMesh.DataProduct;

/// <summary>
/// Extensions for registering features and configuring the web application to use them.
/// </summary>
public static class FeaturesStartupExtensions
{
    /// <summary>
    /// Registers features to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    public static IServiceCollection AddFeatures(this IServiceCollection services, DataProductOptions options)
    {
        _ = services.AddDataStore(options.DataStoreOptions);
        services.AddApis(options);
        //_ = services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }

    /// <summary>
    /// Configures the web application to use enabled features.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    public static WebApplication UseFeatures(this WebApplication app, DataProductOptions options)
    {
        _ = app.UseDataStore(options.DataStoreOptions);
        app.UseApis(options);

        return app;
    }
}
