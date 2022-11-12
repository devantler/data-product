using Dapr.Client;
using Devantler.DataMesh.DataProduct.Models;

namespace Devantler.DataMesh.DataProduct.Services;

public class DaprService<T> : IService<T> where T : Model
{
    private readonly DaprClient _daprClient;

    public DaprService(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    public async Task<IEnumerable<T>> ReadPagedAsync(int page, int pageSize,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<T>> Query(string query, CancellationToken cancellationToken = default)
    {
        var queryResponse = await _daprClient.QueryStateAsync<T>("db", query, cancellationToken: cancellationToken);
        return queryResponse.Results.Select(x => x.Data);
    }

    public async Task<T> ReadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _daprClient.GetStateAsync<T>("db", id.ToString(), cancellationToken: cancellationToken);
    }

    public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _daprClient.SaveStateAsync("db", entity.Id.ToString(), entity, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _daprClient.SaveStateAsync("db", entity.Id.ToString(), entity, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _daprClient.DeleteStateAsync("db", id.ToString(), cancellationToken: cancellationToken);
    }
}
