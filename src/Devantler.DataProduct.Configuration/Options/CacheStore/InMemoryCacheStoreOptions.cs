namespace Devantler.DataProduct.Configuration.Options.CacheStore;

/// <summary>
/// Options to configure an in-memory cache store for the data product.
/// </summary>
public class InMemoryCacheStoreOptions : CacheStoreOptions
{
    /// <inheritdoc/>
    public override CacheStoreType Type { get; set; } = CacheStoreType.InMemory;
}