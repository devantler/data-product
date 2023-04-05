using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Features.Apis.Rest.DataStoreProxy;

/// <summary>
/// A controller that allows executing raw queries against a datastore.
/// </summary>
public interface IDataStoreProxyController
{
    /// <summary>
    /// Execute a raw query against the datastore.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult> ExecuteAsync(string query, object[] parameters, CancellationToken cancellationToken = default);
}
