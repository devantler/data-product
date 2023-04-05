using Devantler.DataMesh.DataProduct.Features.DataStore.Repositories;

namespace Devantler.DataMesh.DataProduct.Features.DataStore.Services;

/// <inheritdoc cref="IDataStoreProxyService"/>
public class DataStoreProxyService : IDataStoreProxyService
{
    readonly IProxyRepository _proxyRepository;

    /// <summary>
    /// Creates a new instance of <see cref="DataStoreProxyService"/>.
    /// </summary>
    /// <param name="proxyRepository"></param>
    public DataStoreProxyService(IProxyRepository proxyRepository)
        => _proxyRepository = proxyRepository;

    /// <inheritdoc />
    public Task<object> ExecuteAsync(string query, object[] parameters, CancellationToken cancellationToken = default)
        => _proxyRepository.ExecuteAsync(query, parameters, cancellationToken);
}

