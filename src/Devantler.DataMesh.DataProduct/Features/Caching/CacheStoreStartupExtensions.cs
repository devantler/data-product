namespace Devantler.DataMesh.DataProduct.Features.Caching;

/// <summary>
/// Extensions to registers a cache store to the DI container and configure the web application to use it.
/// </summary>
public static class CachingStartupExtensions
{
    /// <summary>
    /// Registers the cache store to the DI container.
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddCaching(this IServiceCollection services)
        => services;


    /// <summary>
    /// Configures the web application to use the cache store.
    /// </summary>
    /// <param name="app"></param>
    public static IApplicationBuilder UseCaching(this IApplicationBuilder app)
        => app;
}