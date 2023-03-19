namespace Devantler.DataMesh.DataProduct.Features.Caching.Services;

/// <summary>
/// A cache store service, which can be used to store and retrieve data from a cache store.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public interface ICacheStoreService<TKey, TValue>
    where TKey : notnull
{
    /// <summary>
    /// Gets a value from the cache store.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TValue?> GetAsync(TKey key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets or sets a value in the cache store.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="valueFactory"></param>
    /// <param name="cancellationToken"></param>
    Task<TValue?> GetOrSetAsync(TKey key, Func<Task<TValue>> valueFactory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets a value in the cache store.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SetAsync(TKey key, TValue value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a value in the cache store.
    /// </summary>
    /// <param name="keys"></param>
    /// <param name="cancellationToken"></param>
    Task RemoveAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken = default);
}