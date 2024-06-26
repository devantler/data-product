namespace Devantler.DataProduct.Configuration.Options.CacheStore;

/// <summary>
/// Options to configure a cache store for the data product.
/// </summary>
public class CacheStoreOptions
{
  /// <summary>
  /// A key to indicate the section in the configuration file that contains the cache store options.
  /// </summary>
  public const string Key = "CacheStore";

  /// <summary>
  /// The type of the cache store.
  /// </summary>
  public virtual CacheStoreType Type { get; set; }

  /// <summary>
  /// The expiration time for cache entries.
  /// </summary>
  /// <remarks>
  /// Supports the following formats: "*d", "*h", "*m", "*s". For example, "1d" for one day, "2h" for two hours, "3m" for three minutes, "4s" for four seconds.
  /// Default value is "5m".
  /// Default format is minutes. For example, "5" for five minutes.
  /// </remarks>
  public virtual string ExpirationTime { get; set; } = "5m";
}
