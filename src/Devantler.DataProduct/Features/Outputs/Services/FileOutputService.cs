using System.Globalization;
using System.Text.Json;
using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.Outputs;
using Devantler.DataProduct.Features.Schemas;
using Microsoft.Extensions.Options;

namespace Devantler.DataProduct.Features.Outputs.Services;

/// <summary>
/// An output that egests data to a local file.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="FileOutputService{TKey, TSchema}"/>.
/// </remarks>
/// <param name="options"></param>
public class FileOutputService<TKey, TSchema>(IOptions<DataProductOptions> options) : IOutputService<TKey, TSchema> where TSchema : class, ISchema<TKey>
{
  readonly IOptions<DataProductOptions> _options = options;

  /// <summary>
  /// Sends data to a file.
  /// </summary>
  /// <param name="schema"></param>
  /// <param name="method"></param>
  /// <param name="cancellationToken"></param>
  public async Task SendAsync(TSchema schema, string method, CancellationToken cancellationToken = default)
  {
    foreach (var output in _options.Value.Outputs)
    {
      if (output is not FileOutputOptions fileOutputOptions)
        continue;

      string filePath = fileOutputOptions.FilePath;

      // Create folder if it doesn't exist
      if (!Directory.Exists(filePath))
      {
        string? directory = System.IO.Path.GetDirectoryName(filePath);
        if (directory is not null)
          _ = Directory.CreateDirectory(directory);
      }

      if (!File.Exists(filePath))
        File.Create(filePath).Dispose();

      string fileContent = JsonSerializer.Serialize(schema);

      await using var streamWriter = File.AppendText(filePath);
      await streamWriter.WriteLineAsync($"// {DateTime.Now.ToString(CultureInfo.InvariantCulture)} - {method}");
      await streamWriter.WriteLineAsync(fileContent);
    }
  }

  /// <summary>
  /// Sends data to a file.
  /// </summary>
  /// <param name="schemas"></param>
  /// <param name="method"></param>
  /// <param name="cancellationToken"></param>
  public async Task SendAsync(IEnumerable<TSchema> schemas, string method, CancellationToken cancellationToken = default)
  {
    foreach (var schema in schemas)
      await SendAsync(schema, method, cancellationToken);
  }
}
