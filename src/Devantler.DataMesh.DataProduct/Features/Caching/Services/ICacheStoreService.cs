namespace Devantler.DataMesh.DataProduct.Features.Caching.Services;

/// <summary>
/// A cache store service, which can be used to store and retrieve data from a cache store.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface ICacheStoreService<TValue>
{
    /// <summary>
    /// Gets a value from the cache store.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TValue?> GetAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets multiple values from the cache store.
    /// </summary>
    Task<IEnumerable<TValue?>> GetAsync(IEnumerable<string> keys, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets or sets a value in the cache store.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="valueFactory"></param>
    /// <param name="cancellationToken"></param>
    Task<TValue?> GetOrSetAsync(string key, Func<Task<TValue>> valueFactory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets a value in the cache store.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SetAsync(string key, TValue value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets multiple values in the cache store.
    /// </summary>
    /// <param name="keys"></param>
    /// <param name="values"></param>    
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SetAsync(IEnumerable<string> keys, IEnumerable<TValue> values, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a value in the cache store.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
}