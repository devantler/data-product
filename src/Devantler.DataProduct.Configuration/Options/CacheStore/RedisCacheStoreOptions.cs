namespace Devantler.DataProduct.Configuration.Options.CacheStore;

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

    int database;

    /// <summary>
    /// The database to use. A number between 0 and 15.
    /// </summary>
    public int Database
    {
        get => database;
        set => database = value is >= 0 and <= 15
            ? value
            : throw new ArgumentOutOfRangeException("A Redis database only supports 15 concurrent databases.");
    }
}