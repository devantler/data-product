namespace Devantler.DataMesh.DataProduct.Configuration.Options.CacheStore;

/// <summary>
/// Options to configure a Redis cache store for the data product.
/// </summary>
public class RedisCacheStoreOptions : ICacheStoreOptions
{
    /// <inheritdoc/>
    public CacheStoreType Type { get; set; } = CacheStoreType.Redis;

    /// <inheritdoc/>
    public string ExpirationTime { get; set; } = "5m";

    /// <summary>
    /// The server to connect to.
    /// </summary>
    public string Server { get; set; } = string.Empty;
}