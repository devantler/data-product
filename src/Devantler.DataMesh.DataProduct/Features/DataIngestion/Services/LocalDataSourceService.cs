using System.Text.Json;
using System.Text.Json.Serialization;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataIngestionSource;
using Devantler.DataMesh.DataProduct.Features.DataStore.Services;
using Devantler.DataMesh.DataProduct.Schemas;
using Microsoft.Extensions.Options;

namespace Devantler.DataMesh.DataProduct.Features.DataIngestion.Services;

/// <summary>
/// A data ingestion source service that ingests data from a local file.
/// </summary>
public class LocalDataIngestionSourceService<TSchema> : IDataIngestionSourceService
    where TSchema : class, Schemas.ISchema
{
    readonly IDataStoreService<TSchema> _dataStoreService;
    readonly DataProductOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="LocalDataIngestionSourceService{TSchema}"/> class.
    /// </summary>
    public LocalDataIngestionSourceService(IServiceScopeFactory scopeFactory)
    {
        var scope = scopeFactory.CreateScope();
        _dataStoreService = scope.ServiceProvider.GetRequiredService<IDataStoreService<TSchema>>();
        _options = scope.ServiceProvider.GetRequiredService<IOptions<DataProductOptions>>().Value;
    }

    /// <inheritdoc/>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var localDataIngestionSources = _options.Services.DataIngestionSources
            .Where(x => x.Type == DataIngestionSourceType.Local)
            .Cast<LocalDataIngestionSourceOptions>()
            .ToList();

        foreach (var dataIngestionSource in localDataIngestionSources)
        {
            string filePath = dataIngestionSource.FilePath;
            var file = new FileInfo(filePath);
            if (!file.Exists)
                throw new FileNotFoundException($"File {filePath} does not exist.");

            string data = await File.ReadAllTextAsync(filePath, cancellationToken);

            var models = file.Extension switch
            {
                ".json" => DeserializeJson(data),
                _ => throw new NotSupportedException($"File extension {file.Extension} is not supported.")
            };

            _ = await _dataStoreService.UpdateMultipleAsync(models, cancellationToken);
        }
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;

    /// <summary>
    /// Deserializes JSON data into a list of <typeparamref name="TSchema"/> models.
    /// </summary>
    static List<TSchema> DeserializeJson(string data)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var json = JsonDocument.Parse(data);
        if (json.RootElement.ValueKind == JsonValueKind.Array)
        {
            return json.Deserialize<List<TSchema>>(options)
                ?? throw new InvalidOperationException($"Failed to deserialize JSON as {typeof(List<TSchema>).Name}.");
        }
        else
        {
            var model = json.Deserialize<TSchema>(options)
                ?? throw new InvalidOperationException($"Failed to deserialize JSON as {typeof(TSchema).Name}.");
            return new List<TSchema> { model };
        }
    }
}