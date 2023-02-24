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
    Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates multiple <typeparamref name="TModel"/>'s in a data store.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    Task<int> CreateManyAsync(IEnumerable<TModel> models, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads a single <typeparamref name="TModel"/> from a data store.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<TModel> ReadAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Read multiple <typeparamref name="TModel"/>'s from a data store.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<TModel>> ReadManyAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads paged <typeparamref name="TModel"/>'s from a data store.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<TModel>> ReadPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads <typeparamref name="TModel"/>'s from a data store with a limit and an offset.
    /// </summary>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<TModel>> ReadListAsync(int limit, int offset, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a single <typeparamref name="TModel"/> in a data store.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    Task<TModel> UpdateAsync(TModel model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates multiple <typeparamref name="TModel"/>'s in a data store.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    Task<int> UpdateManyAsync(IEnumerable<TModel> models, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a single <typeparamref name="TModel"/> from a data store.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<TModel> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple <typeparamref name="TModel"/>'s from a data store.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<int> DeleteManyAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a query against the data store for matching <typeparamref name="TModel"/>'s.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<TModel>> QueryAsync(string query, CancellationToken cancellationToken = default);
}
