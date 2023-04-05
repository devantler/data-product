namespace Devantler.DataMesh.DataProduct.Features.DataStore.Repositories;

/// <summary>
/// A repository that allows executing raw queries against a datastore.
/// </summary>
public interface IProxyRepository
{
    /// <summary>
    /// Execute a raw query against the datastore.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    Task<object> ExecuteAsync(string query, object[] parameters, CancellationToken cancellationToken = default);
}