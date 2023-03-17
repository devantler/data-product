using System.Text.Json;
using System.Text.Json.Serialization;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataIngestors;
using Devantler.DataMesh.DataProduct.Features.DataIngestion.JsonConverters;
using Devantler.DataMesh.DataProduct.Features.DataStore.Services;
using Microsoft.Extensions.Options;

namespace Devantler.DataMesh.DataProduct.Features.DataIngestion.Services;

/// <summary>
/// A data ingestor that ingests data from a local file.
/// </summary>
<<<<<<< HEAD:src/Devantler.DataMesh.DataProduct/Features/DataIngestion/Services/LocalDataIngestorService.cs
public class LocalDataIngestorService<TSchema> : BackgroundService
    where TSchema : class, Schemas.ISchema
=======
public class LocalDataIngestor<TKey, TSchema> : BackgroundService, IDataIngestor
    where TSchema : class, Schemas.ISchema<TKey>
>>>>>>> main:src/Devantler.DataMesh.DataProduct/Features/DataIngestion/Ingestors/LocalDataIngestor.cs
{
    readonly IDataStoreService<TKey, TSchema> _dataStoreService;
    readonly DataProductOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="LocalDataIngestor{TKey, TSchema}"/> class.
    /// </summary>
    public LocalDataIngestorService(IServiceScopeFactory scopeFactory)
    {
        var scope = scopeFactory.CreateScope();
        _dataStoreService = scope.ServiceProvider.GetRequiredService<IDataStoreService<TKey, TSchema>>();
        _options = scope.ServiceProvider.GetRequiredService<IOptions<DataProductOptions>>().Value;
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var localDataIngestionSources = _options.Services.DataIngestors
            .Where(x => x.Type == DataIngestorType.Local)
            .Cast<LocalDataIngestorOptions>()
            .ToList();

        var schemas = new List<TSchema>();

        foreach (string? filePath in localDataIngestionSources.Select(dataIngestionSource => dataIngestionSource.FilePath))
        {
            var file = new FileInfo(filePath);
            if (!file.Exists)
                throw new FileNotFoundException($"File {filePath} does not exist.");

            string data = await File.ReadAllTextAsync(filePath, stoppingToken);

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
#pragma warning disable IL2026 // As the schema types are generated, and thus persisted at runtime, the trimmer is able to detect the types and serialize them.
                        JsonValueKind.Array => json.Deserialize<List<TSchema>>(options)
                            ?? throw new InvalidOperationException($"Failed to deserialize JSON as {typeof(List<TSchema>).Name}."),
                        JsonValueKind.Object => new List<TSchema> { json.Deserialize<TSchema>(options)
                            ?? throw new InvalidOperationException($"Failed to deserialize JSON as {typeof(TSchema).Name}.")
                        },
#pragma warning restore IL2026
                        _ => throw new NotSupportedException($"JSON value kind {json.RootElement.ValueKind} is not supported."),
                    };
                    schemas.AddRange(deserializedSchemas);
                    break;
                default:
                    throw new NotSupportedException($"File extension {file.Extension} is not supported.");
            }
        }
        _ = await _dataStoreService.CreateMultipleAsync(schemas, stoppingToken);
    }
}