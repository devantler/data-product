using System.Text.Json;
using Dapr.Client;
using Devantler.DataMesh.Core.Models;
using Devantler.DataMesh.DataProduct.Features.StateStore.Models;
using Microsoft.Extensions.Options;

namespace Devantler.DataMesh.DataProduct.Features.StateStore.Services;

public class StateStoreServiceBase<T> : IStateStoreService<T> where T : IModel
{
    private readonly DaprClient _daprClient;
    private readonly IOptionsMonitor<StateStoreOptions> _stateStoreOptions;

    public StateStoreServiceBase(DaprClient daprClient, IOptionsMonitor<StateStoreOptions> stateStoreOptions)
    {
        _daprClient = daprClient;
        _stateStoreOptions = stateStoreOptions;
    }
    public async Task CreateAsync(T model, CancellationToken cancellationToken = default) =>
        await _daprClient.SaveStateAsync(_stateStoreOptions.CurrentValue.Name, model.Id.ToString(), model, cancellationToken: cancellationToken);

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _daprClient.DeleteStateAsync(_stateStoreOptions.CurrentValue.Name, id.ToString(), cancellationToken: cancellationToken);

    public async Task<IEnumerable<T>> Query(string query, CancellationToken cancellationToken = default)
    {
        var queryResponse = await _daprClient.QueryStateAsync<T>(_stateStoreOptions.CurrentValue.Name, query, cancellationToken: cancellationToken);
        return queryResponse.Results.Select(x => x.Data);
    }

    public Task<T> ReadAsync(Guid id, CancellationToken cancellationToken = default) =>
        _daprClient.GetStateAsync<T>(_stateStoreOptions.CurrentValue.Name, id.ToString(), cancellationToken: cancellationToken);

    public async Task<IEnumerable<T>> ReadBulkAsync(Guid[] ids, CancellationToken cancellationToken = default)
    {
        var result = await _daprClient.GetBulkStateAsync(
            _stateStoreOptions.CurrentValue.Name, 
            ids.Select(x => x.ToString()).ToList(), 
            _stateStoreOptions.CurrentValue.Parallelism, 
            cancellationToken: cancellationToken
        );
        return result.Select(x => JsonSerializer.Deserialize<T>(x.Value) ?? throw new Exception("Failed to deserialize state store value"));
    }

    public async Task UpdateAsync(T model, CancellationToken cancellationToken = default) =>
        await _daprClient.SaveStateAsync(_stateStoreOptions.CurrentValue.Name, model.Id.ToString(), model, cancellationToken: cancellationToken);
}
