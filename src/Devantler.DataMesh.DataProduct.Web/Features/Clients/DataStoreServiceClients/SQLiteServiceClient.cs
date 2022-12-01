namespace Devantler.DataMesh.DataProduct.Web.Features.Clients.DataStoreServiceClients;

public class SQLiteServiceClient<T> : IDataStoreServiceClient<T>
{
    private readonly HttpClient _httpClient;

    public SQLiteServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<T> CreateAsync(T model, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<T> ReadAsync(string id, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<T> UpdateAsync(T model, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    // public async Task CreateAsync(T model, CancellationToken cancellationToken = default)
    // {
    //     var response = await _httpClient.PostAsJsonAsync(string.Empty, model, cancellationToken);
    //     response.EnsureSuccessStatusCode();
    // }

    // public async Task<T> ReadAsync(string id, CancellationToken cancellationToken = default) =>
    //     await _httpClient.GetFromJsonAsync<T>(id, cancellationToken);

    // public async Task UpdateAsync(T model, CancellationToken cancellationToken = default)
    // {
    //     var response = await _httpClient.PutAsJsonAsync(string.Empty, model, cancellationToken);
    //     response.EnsureSuccessStatusCode();
    // }

    // public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
    // {
    //     var response = await _httpClient.DeleteAsync(id, cancellationToken);
    //     response.EnsureSuccessStatusCode();
    //     return response.IsSuccessStatusCode;
    // }

    // public async Task<IEnumerable<T>> Query(string query, CancellationToken cancellationToken = default) =>
    //     await _httpClient.GetFromJsonAsync<IEnumerable<T>>(query, cancellationToken) ?? Enumerable.Empty<T>();
}
