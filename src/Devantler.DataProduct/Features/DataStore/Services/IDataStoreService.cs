using Devantler.DataProduct.Features.Schemas;

namespace Devantler.DataProduct.Features.DataStore.Services;

/// <summary>
/// Generic interface for services that interact with datastores.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TSchema"></typeparam>
public interface IDataStoreService<TKey, TSchema> where TSchema : class, ISchema<TKey>
{
    /// <summary>
    /// Creates a single <typeparamref name="TSchema"/> in a data store.
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="cancellationToken"></param>
    Task<TSchema> CreateSingleAsync(TSchema schema, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates multiple <typeparamref name="TSchema"/>'s in a data store.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="insertIfNotExists"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<TSchema>> CreateMultipleAsync(IEnumerable<TSchema> models, bool insertIfNotExists, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads a single <typeparamref name="TSchema"/> from a data store.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<TSchema> ReadSingleAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all <typeparamref name="TSchema"/>'s from a data store.
    /// </summary>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<TSchema>> ReadAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Read multiple <typeparamref name="TSchema"/>'s from a data store.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<TSchema>> ReadMultipleAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads paged <typeparamref name="TSchema"/>'s from a data store.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<TSchema>> ReadMultipleWithPaginationAsync(int page, int pageSize,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads <typeparamref name="TSchema"/>'s from a data store with a limit and an offset.
    /// </summary>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<TSchema>> ReadMultipleWithOffsetAsync(int limit, int offset,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a single <typeparamref name="TSchema"/> in a data store.
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="cancellationToken"></param>
    Task UpdateSingleAsync(TSchema schema, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates multiple <typeparamref name="TSchema"/>'s in a data store.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    Task UpdateMultipleAsync(IEnumerable<TSchema> models, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a single <typeparamref name="TSchema"/> from a data store.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task DeleteSingleAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple <typeparamref name="TSchema"/>'s from a data store.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task DeleteMultipleAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);
}
