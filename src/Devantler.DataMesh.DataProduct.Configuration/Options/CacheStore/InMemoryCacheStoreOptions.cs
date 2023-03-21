namespace Devantler.DataMesh.DataProduct.Configuration.Options.CacheStore;

/// <summary>
/// Options to configure an in-memory cache store for the data product.
/// </summary>
public class InMemoryCacheStoreOptions : ICacheStoreOptions
{
    /// <inheritdoc/>
    public CacheStoreType Type { get; set; } = CacheStoreType.InMemory;

    /// <inheritdoc/>
    public string ExpirationTime { get; set; } = "5m";
}