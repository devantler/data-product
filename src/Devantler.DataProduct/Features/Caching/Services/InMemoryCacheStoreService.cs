using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.CacheStore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Devantler.DataProduct.Features.Caching.Services;

/// <summary>
/// An in-memory cache store service, which can be used to store and retrieve data from memory.
/// </summary>
/// <remarks>
/// Creates a new instance of the <see cref="InMemoryCacheStoreService{TValue}"/> class.
/// </remarks>
public class InMemoryCacheStoreService<TValue>(IMemoryCache memoryCache, IOptions<DataProductOptions> options) : ICacheStoreService<TValue>
{
  readonly IMemoryCache _memoryCache = memoryCache;
  readonly DataProductOptions _options = options.Value;

  /// <inheritdoc />
  public Task<TValue?> GetAsync(string key, CancellationToken cancellationToken = default)
  {
    var cacheEntry = _memoryCache.Get<TValue>(key);
    return Task.FromResult(cacheEntry);
  }

  /// <inheritdoc />
  public Task<TValue?> GetOrSetAsync(string key, Func<Task<TValue>> valueFactory, CancellationToken cancellationToken = default)
  {
    return _memoryCache.GetOrCreateAsync(key, async entry =>
    {
      entry.SlidingExpiration = _options.CacheStore.ExpirationTime.ToTimeSpan();
      return await valueFactory();
    });
  }

  /// <inheritdoc />
  public Task SetAsync(string key, TValue value, CancellationToken cancellationToken = default)
  {
    _ = _memoryCache.Set(key, value, new MemoryCacheEntryOptions
    {
      SlidingExpiration = _options.CacheStore.ExpirationTime.ToTimeSpan()
    });
    return Task.CompletedTask;
  }

  /// <inheritdoc />
  public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
  {
    _memoryCache.Remove(key);
    return Task.CompletedTask;
  }

  /// <inheritdoc />
  public Task<IEnumerable<TValue?>> GetAsync(IEnumerable<string> keys, CancellationToken cancellationToken = default)
  {
    var cacheEntries = keys.Select(_memoryCache.Get<TValue>);
    return Task.FromResult(cacheEntries);
  }

  /// <inheritdoc />
  public async Task SetAsync(IEnumerable<string> keys, IEnumerable<TValue> values, CancellationToken cancellationToken = default)
  {
    foreach (var (key, value) in keys.Zip(values, (key, value) => (key, value)))
    {
      await SetAsync(key, value, cancellationToken);
    }
  }
}
