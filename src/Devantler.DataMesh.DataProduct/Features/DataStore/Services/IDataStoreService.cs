namespace Devantler.DataMesh.DataProduct.DataStore.Services;

/// <summary>
/// Generic interface for services that interact with datastores.
/// </summary>
/// <typeparam name="TModel"></typeparam>
public interface IDataStoreService<TModel> where TModel : class
{
    /// <summary>
    /// Creates a single <typeparamref name="TModel"/> in a data store.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    Task<TModel> CreateSingleAsync(TModel model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates multiple <typeparamref name="TModel"/>'s in a data store.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    Task<int> CreateMultipleAsync(IEnumerable<TModel> models, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads a single <typeparamref name="TModel"/> from a data store.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<TModel> GetSingleAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all <typeparamref name="TModel"/>'s from a data store.
    /// </summary>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all <typeparamref name="TModel"/>'s from a data store as queryable objects.
    /// </summary>
    /// <param name="cancellationToken"></param>
    Task<IQueryable<TModel>> GetAllAsQueryableAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Read multiple <typeparamref name="TModel"/>'s from a data store.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<TModel>> GetMultipleAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads paged <typeparamref name="TModel"/>'s from a data store.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<TModel>> GetMultipleWithPaginationAsync(int page, int pageSize,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads <typeparamref name="TModel"/>'s from a data store with a limit and an offset.
    /// </summary>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<TModel>> GetMultipleWithLimitAsync(int limit, int offset,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a single <typeparamref name="TModel"/> in a data store.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    Task<TModel> UpdateSingleAsync(TModel model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates multiple <typeparamref name="TModel"/>'s in a data store.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    Task<int> UpdateMultipleAsync(IEnumerable<TModel> models, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a single <typeparamref name="TModel"/> from a data store.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<TModel> DeleteSingleAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple <typeparamref name="TModel"/>'s from a data store.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<int> DeleteMultipleAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}
