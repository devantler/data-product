namespace Devantler.DataMesh.DataProduct.Configuration.Options.CacheStore;

/// <summary>
/// Options to configure a Redis cache store for the data product.
/// </summary>
public class RedisCacheStoreOptions : ICacheStoreOptions
{
    /// <inheritdoc/>
    public string Name { get; set; } = "Redis";

    /// <inheritdoc/>
    public CacheStoreType Type { get; set; } = CacheStoreType.Redis;

    /// <inheritdoc/>
    public string ExpirationTime { get; set; } = "5m";

    /// <summary>
    /// The Redis server to connect to.
    /// </summary>
    public string RedisServer { get; set; } = string.Empty;
}