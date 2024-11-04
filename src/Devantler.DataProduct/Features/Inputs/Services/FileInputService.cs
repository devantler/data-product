using System.Text.Json;
using System.Text.Json.Serialization;
using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.Inputs;
using Devantler.DataProduct.Features.DataStore.Services;
using Devantler.DataProduct.Features.Inputs.JsonConverters;
using Devantler.DataProduct.Features.Schemas;
using Microsoft.Extensions.Options;

namespace Devantler.DataProduct.Features.Inputs.Services;

/// <summary>
/// A input that ingests data from a local file.
/// </summary>
public class FileInputService<TKey, TSchema> : BackgroundService
    where TSchema : class, ISchema<TKey>
{
  readonly IDataStoreService<TKey, TSchema> _dataStoreService;
  readonly DataProductOptions _options;

  /// <summary>
  /// Initializes a new instance of the <see cref="FileInputService{TKey, TSchema}"/> class.
  /// </summary>
  public FileInputService(IServiceScopeFactory scopeFactory)
  {
    var scope = scopeFactory.CreateScope();
    _dataStoreService = scope.ServiceProvider.GetRequiredService<IDataStoreService<TKey, TSchema>>();
    _options = scope.ServiceProvider.GetRequiredService<IOptions<DataProductOptions>>().Value;
  }

  /// <inheritdoc />
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    var localInputsSources = _options.Inputs
        .Where(x => x.Type == InputType.File)
        .Cast<FileInputOptions>()
        .ToList();

    var schemas = new List<TSchema>();

    foreach (string? filePath in localInputsSources.Select(InputsSource => InputsSource.FilePath))
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
            JsonValueKind.Array => json.Deserialize<List<TSchema>>(options)
                ?? throw new InvalidOperationException($"Failed to deserialize JSON as {typeof(List<TSchema>).Name}."),
            JsonValueKind.Object => [ json.Deserialize<TSchema>(options)
                            ?? throw new InvalidOperationException($"Failed to deserialize JSON as {typeof(TSchema).Name}.")
                        ],
            JsonValueKind.Undefined => throw new NotImplementedException(),
            JsonValueKind.String => throw new NotImplementedException(),
            JsonValueKind.Number => throw new NotImplementedException(),
            JsonValueKind.True => throw new NotImplementedException(),
            JsonValueKind.False => throw new NotImplementedException(),
            JsonValueKind.Null => throw new NotImplementedException(),
            _ => throw new NotSupportedException($"JSON value kind {json.RootElement.ValueKind} is not supported.")
          };
          schemas.AddRange(deserializedSchemas);
          break;
        default:
          throw new NotSupportedException($"File extension {file.Extension} is not supported.");
      }
    }
    _ = await _dataStoreService.CreateMultipleAsync(schemas.Distinct(), true, stoppingToken);
  }

  /// <inheritdoc />
  public override async Task StopAsync(CancellationToken cancellationToken = default)
      => await base.StopAsync(cancellationToken);
}
