using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.CacheStore;
using StackExchange.Redis;

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
            CacheStoreType.Redis => services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(((RedisCacheStoreOptions)options.CacheStore).Server)),
            _ => throw new NotSupportedException($"Cache store type '{options.CacheStore.Type}' is not supported.")
        };

        services.AddGeneratedServiceRegistrations(options);
        return services;
    }

    static partial void AddGeneratedServiceRegistrations(this IServiceCollection services, DataProductOptions options);
}