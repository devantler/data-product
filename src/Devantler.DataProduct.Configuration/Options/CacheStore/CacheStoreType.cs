namespace Devantler.DataProduct.Configuration.Options.CacheStore;

/// <summary>
/// Supported cache stores.
/// </summary>
public enum CacheStoreType
{
    /// <summary>
    /// An in-memory cache store.
    /// </summary>
    InMemory,

    /// <summary>
    /// A distributed Redis cache store.
    /// </summary>
    Redis
}