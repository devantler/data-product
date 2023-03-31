using System.Text.Json;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.CacheStore;
using StackExchange.Redis;

namespace Devantler.DataMesh.DataProduct.Features.Caching.Services;

/// <summary>
/// A cache store that uses Redis as the backing store.
/// </summary>
public class RedisCacheStoreService<TValue> : ICacheStoreService<TValue>
{
    readonly IDatabase _redis;
    readonly DataProductOptions _options;
    /// <summary>
    /// Initializes a new instance of the <see cref="RedisCacheStoreService{TValue}"/> class.
    /// </summary>
    /// <param name="redisConnectionMultiplexer"></param>
    /// <param name="options"></param>
    public RedisCacheStoreService(IConnectionMultiplexer redisConnectionMultiplexer, DataProductOptions options)
    {
        _redis = redisConnectionMultiplexer.GetDatabase();
        _options = options;
    }

    /// <inheritdoc/>
    public async Task<TValue?> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        var value = await _redis.StringGetAsync(key);
        return value.HasValue
            ? JsonSerializer.Deserialize<TValue>(value.ToString())
            : default;
    }

    /// <inheritdoc/>
    public async Task<TValue?> GetOrSetAsync(string key, Func<Task<TValue>> valueFactory, CancellationToken cancellationToken = default)
    {
        var value = await _redis.StringGetSetAsync(key, JsonSerializer.Serialize(await valueFactory()));
        _ = _redis.KeyExpireAsync(key, _options.CacheStore.ExpirationTime.ToTimeSpan());
        return value.HasValue
            ? JsonSerializer.Deserialize<TValue>(value.ToString())
            : default;
    }

    /// <inheritdoc/>
    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        => _redis.KeyDeleteAsync(key);

    /// <inheritdoc/>
    public Task SetAsync(string key, TValue value, CancellationToken cancellationToken = default)
    {
        string expirationTime = _options.CacheStore.ExpirationTime;
        return _redis.StringSetAsync(key, JsonSerializer.Serialize(value), expirationTime.ToTimeSpan());
    }
}