using Devantler.DataMesh.DataProduct.Configuration.Options;
namespace Devantler.DataMesh.DataProduct;

/// <summary>
/// Extensions for registering data sources and configuring the web application to use them.
/// </summary>
public static class DataSourceStartupExtensions
{
    /// <summary>
    /// Registers data sources to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <param name="environment"></param>
    public static IServiceCollection AddDataSources(this IServiceCollection services, DataProductOptions options, IWebHostEnvironment environment)
    {
        return services;
    }

    /// <summary>
    /// Configures the web application to use enabled data sources.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    public static WebApplication UseDataSources(this WebApplication app, DataProductOptions options)
    {
        return app;
    }
}
