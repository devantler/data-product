using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.CacheStore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Devantler.DataMesh.DataProduct.Features.Caching.Services;

/// <summary>
/// An in-memory cache store service, which can be used to store and retrieve data from memory.
/// </summary>
public class InMemoryCacheStoreService<TValue> : ICacheStoreService<TValue>
{
    readonly IMemoryCache _memoryCache;
    readonly DataProductOptions _options;

    /// <summary>
    /// Creates a new instance of the <see cref="InMemoryCacheStoreService{TValue}"/> class.
    /// </summary>
    public InMemoryCacheStoreService(IMemoryCache memoryCache, IOptions<DataProductOptions> options)
    {
        _memoryCache = memoryCache;
        _options = options.Value;
    }

    /// <inheritdoc />
    public Task<TValue?> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        var cacheEntry = _memoryCache.Get<TValue>(key);
        return Task.FromResult(cacheEntry);
    }

    /// <inheritdoc />
    public Task<TValue?> GetOrSetAsync(string key, Func<Task<TValue>> valueFactory, CancellationToken cancellationToken = default)
    {
        var cacheEntry = _memoryCache.GetOrCreateAsync(key, async entry =>
        {
            entry.SlidingExpiration = _options.CacheStore.ExpirationTime.ToTimeSpan();
            return await valueFactory();
        });
        return cacheEntry;
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
}