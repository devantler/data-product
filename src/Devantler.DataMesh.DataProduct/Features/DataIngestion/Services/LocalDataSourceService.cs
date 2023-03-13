using System.Text.Json;
using System.Text.Json.Serialization;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataIngestionSource;
using Devantler.DataMesh.DataProduct.Features.DataStore.Services;
using Devantler.DataMesh.DataProduct.JsonConverters;
using Microsoft.Extensions.Options;

namespace Devantler.DataMesh.DataProduct.Features.DataIngestion.Services;

/// <summary>
/// A data ingestor that ingests data from a local file.
/// </summary>
public class LocalDataIngestorService<TSchema> : IDataIngestorService
    where TSchema : class, Schemas.ISchema
{
    readonly IDataStoreService<TSchema> _dataStoreService;
    readonly DataProductOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="LocalDataIngestorService{TSchema}"/> class.
    /// </summary>
    public LocalDataIngestorService(IServiceScopeFactory scopeFactory)
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

        var schemas = new List<TSchema>();

        foreach (var dataIngestionSource in localDataIngestionSources)
        {
            string filePath = dataIngestionSource.FilePath;
            var file = new FileInfo(filePath);
            if (!file.Exists)
                throw new FileNotFoundException($"File {filePath} does not exist.");

            string data = await File.ReadAllTextAsync(filePath, cancellationToken);

            switch (file.Extension)
            {
                case ".json":
                    var options = new JsonSerializerOptions
                    {
                        NumberHandling = JsonNumberHandling.AllowReadingFromString,
                        Converters = { new NumberToStringConverter() }
                    };

                    var json = JsonDocument.Parse(data);
                    var deserializedSchemas = json.RootElement.ValueKind switch
                    {
                        JsonValueKind.Array => json.Deserialize<List<TSchema>>(options)
                            ?? throw new InvalidOperationException($"Failed to deserialize JSON as {typeof(List<TSchema>).Name}."),
                        JsonValueKind.Object => new List<TSchema> { json.Deserialize<TSchema>(options)
                            ?? throw new InvalidOperationException($"Failed to deserialize JSON as {typeof(TSchema).Name}.")
                        },
                        _ => throw new NotSupportedException($"JSON value kind {json.RootElement.ValueKind} is not supported."),
                    };
                    schemas.AddRange(deserializedSchemas);
                    break;
                default:
                    throw new NotSupportedException($"File extension {file.Extension} is not supported.");
            }

        }
        _ = await _dataStoreService.CreateMultipleAsync(schemas, cancellationToken);
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}