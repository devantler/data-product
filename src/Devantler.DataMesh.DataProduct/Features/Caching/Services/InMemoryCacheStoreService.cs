using Devantler.DataMesh.DataProduct.Configuration.Extensions;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Devantler.DataMesh.DataProduct.Features.Caching.Services;

/// <summary>
/// An in-memory cache store service, which can be used to store and retrieve data from memory.
/// </summary>
public class InMemoryCacheStoreService<TKey, TValue> : ICacheStoreService<TKey, TValue>
    where TKey : notnull
{
    private readonly IMemoryCache _memoryCache;
    private readonly DataProductOptions _options;

    /// <summary>
    /// Creates a new instance of the <see cref="InMemoryCacheStoreService{TKey, TValue}"/> class.
    /// </summary>
    public InMemoryCacheStoreService(IMemoryCache memoryCache, IOptions<DataProductOptions> options)
    {
        _memoryCache = memoryCache;
        _options = options.Value;
    }

    /// <inheritdoc />
    public Task<TValue?> GetAsync(TKey key, CancellationToken cancellationToken = default)
    {
        var cacheEntry = _memoryCache.Get<TValue>(key);
        return Task.FromResult(cacheEntry);
    }

    /// <inheritdoc />
    public Task<TValue?> GetOrSetAsync(TKey key, Func<Task<TValue>> valueFactory, CancellationToken cancellationToken = default)
    {
        var cacheEntry = _memoryCache.GetOrCreateAsync(key, async entry =>
        {
            entry.SlidingExpiration = _options.CacheStore.ExpirationTime.ToTimeSpan();
            return await valueFactory();
        });
        return cacheEntry;
    }

    /// <inheritdoc />
    public Task SetAsync(TKey key, TValue value, CancellationToken cancellationToken = default)
    {
        _ = _memoryCache.Set(key, value, new MemoryCacheEntryOptions
        {
            SlidingExpiration = _options.CacheStore.ExpirationTime.ToTimeSpan()
        });
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task RemoveAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken = default)
    {
        foreach (var key in keys)
        {
            _memoryCache.Remove(key);
        }
        return Task.CompletedTask;
    }
}