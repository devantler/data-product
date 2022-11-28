namespace Devantler.DataMesh.DataProduct.Clients.DatabaseClients.SQLite;

public class SQLiteServiceClient<T> : ISQLiteServiceClient<T>
{
    private readonly HttpClient _httpClient;

    public SQLiteServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateAsync(T model, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(string.Empty, model, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<T?> ReadAsync(string id, CancellationToken cancellationToken = default) => 
        await _httpClient.GetFromJsonAsync<T>(id, cancellationToken);

    public async Task UpdateAsync(T model, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync(string.Empty, model, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.DeleteAsync(id, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<T>> Query(string query, CancellationToken cancellationToken = default) => 
        await _httpClient.GetFromJsonAsync<IEnumerable<T>>(query, cancellationToken) ?? Enumerable.Empty<T>();
}
