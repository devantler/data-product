namespace Devantler.DataMesh.DataProduct.Configuration.Options.CacheStore;

/// <summary>
/// Options to configure a Redis cache store for the data product.
/// </summary>
public class RedisCacheStoreOptions : CacheStoreOptions
{
    /// <inheritdoc/>
    public override CacheStoreType Type { get; set; } = CacheStoreType.Redis;

    /// <summary>
    /// The server to connect to.
    /// </summary>
    public string Server { get; set; } = string.Empty;
}