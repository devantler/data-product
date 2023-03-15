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
    /// Gets a value from the cache store.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<TValue>> GetMultipleAsync(TKey key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets a value in the cache store.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SetAsync(TKey key, TValue value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets a value in the cache store.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SetMultipleAsync(TKey key, IEnumerable<TValue> values, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a value from the cache store.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(TKey key, CancellationToken cancellationToken = default);
}