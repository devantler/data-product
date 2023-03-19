namespace Devantler.DataMesh.DataProduct.Features.Caching.Services;

/// <summary>
/// A generic interface for cache store services.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public interface ICacheStoreService<TKey, TValue>
{
    /// <summary>
    /// Gets a value from the cache store.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TValue> GetAsync(TKey key, CancellationToken cancellationToken = default);

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