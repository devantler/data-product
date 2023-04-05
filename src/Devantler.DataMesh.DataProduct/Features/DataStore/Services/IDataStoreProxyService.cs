namespace Devantler.DataMesh.DataProduct.Features.DataStore.Services;

/// <summary>
/// A service that allows executing raw queries against a datastore.
/// </summary>
public interface IDataStoreProxyService
{
    /// <summary>
    /// Execute a raw query against the datastore.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    Task<object> ExecuteAsync(string query, object[] parameters, CancellationToken cancellationToken = default);
}