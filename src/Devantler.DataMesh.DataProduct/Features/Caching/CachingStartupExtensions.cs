using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.CacheStore;

namespace Devantler.DataMesh.DataProduct.Features.Caching;

/// <summary>
/// Extensions to registers a cache store to the DI container and configure the web application to use it.
/// </summary>
public static partial class CachingStartupExtensions
{
    /// <summary>
    /// Registers the cache store to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    public static IServiceCollection AddCaching(this IServiceCollection services, DataProductOptions options)
    {
        _ = options.CacheStore.Type switch
        {
            CacheStoreType.InMemory => services.AddMemoryCache(),
            CacheStoreType.Redis => throw new NotSupportedException("Redis cache store is not yet supported."),
            _ => throw new NotSupportedException($"Cache store type '{options.CacheStore.Type}' is not supported.")
        };

        services.AddGeneratedServiceRegistrations(options);
        return services;
    }

    static partial void AddGeneratedServiceRegistrations(this IServiceCollection services, DataProductOptions options);
}