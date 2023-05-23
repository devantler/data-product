using System.Text.Json;
using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.CacheStore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Devantler.DataProduct.Features.Caching.Services;

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
    public RedisCacheStoreService(IConnectionMultiplexer redisConnectionMultiplexer, IOptions<DataProductOptions> options)
    {
        _options = options.Value;
        _redis = redisConnectionMultiplexer.GetDatabase(((RedisCacheStoreOptions)_options.CacheStore).Database);
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
    public async Task<IEnumerable<TValue?>> GetAsync(IEnumerable<string> keys, CancellationToken cancellationToken = default)
    {
        var redisKeys = keys.Select(k => (RedisKey)k).ToArray();

        var redisValues = await _redis.StringGetAsync(redisKeys);

        return redisValues.Select(v => v.HasValue ? JsonSerializer.Deserialize<TValue>(v.ToString()) : default);
    }

    /// <inheritdoc/>
    public async Task<TValue?> GetOrSetAsync(string key, Func<Task<TValue>> valueFactory, CancellationToken cancellationToken = default)
    {
        var value = await GetAsync(key, cancellationToken);
        if (value is not null)
            return value;

        value = await valueFactory();
        await SetAsync(key, value, cancellationToken);
        return value;
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

    /// <inheritdoc/>
    public async Task SetAsync(IEnumerable<string> keys, IEnumerable<TValue> values, CancellationToken cancellationToken = default)
    {
        foreach ((string key, var value) in keys.Zip(values, (k, v) => (k, v)))
        {
            await SetAsync(key, value, cancellationToken);
        }
    }
}