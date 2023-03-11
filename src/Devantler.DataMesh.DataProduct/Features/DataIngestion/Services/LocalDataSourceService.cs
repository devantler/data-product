using System.Text.Json;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataIngestionSource;
using Devantler.DataMesh.DataProduct.Features.DataStore.Services;
using Microsoft.Extensions.Options;

namespace Devantler.DataMesh.DataProduct.Features.DataIngestion.Services;

/// <summary>
/// A data ingestion source service that ingests data from a local file.
/// </summary>
public class LocalDataIngestionSourceService<TSchema> : IDataIngestionSourceService
    where TSchema : class, ISchema
{
    readonly IDataStoreService<TSchema> _dataStoreService;
    readonly DataProductOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="LocalDataIngestionSourceService{TSchema}"/> class.
    /// </summary>
    public LocalDataIngestionSourceService(
        IDataStoreService<TSchema> dataStoreService,
        IOptions<DataProductOptions> options
    )
    {
        _dataStoreService = dataStoreService;
        _options = options.Value;
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

            string data = File.ReadAllText(filePath);

            var models = file.Extension switch
            {
                ".json" => JsonSerializer.Deserialize<List<TSchema>>(data)
                    ?? throw new InvalidOperationException($"Failed to deserialize {filePath} as JSON."),
                _ => throw new NotSupportedException($"File extension {file.Extension} is not supported.")
            };

            _ = await _dataStoreService.UpdateMultipleAsync(models, cancellationToken);
        }
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}