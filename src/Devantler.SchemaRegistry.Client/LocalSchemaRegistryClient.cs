using Chr.Avro.Abstract;
using Chr.Avro.Representation;
using Devantler.SchemaRegistry.Client.Models;

namespace Devantler.SchemaRegistry.Client;

/// <summary>
/// A Local schema registry client.
/// </summary>
public class LocalSchemaRegistryClient : ISchemaRegistryClient
{
  readonly LocalSchemaRegistryOptions _options;

  /// <summary>
  /// A constructor to construct a Local schema registry client.
  /// </summary>
  /// <param name="options"></param>
  public LocalSchemaRegistryClient(Action<LocalSchemaRegistryOptions> options)
  {
    _options = new LocalSchemaRegistryOptions();
    options(_options);
  }

  /// <summary>
  /// A constructor to construct a Local schema registry client.
  /// </summary>
  /// <param name="options"></param>
  public LocalSchemaRegistryClient(LocalSchemaRegistryOptions options)
      => _options = options;

  /// <inheritdoc/>
  public async Task<Schema> GetSchemaAsync(string subject, int version, CancellationToken cancellationToken = default)
  {
    string schemaString = await GetSchemaStringAsync(subject, version, cancellationToken);

    var schemaReader = new JsonSchemaReader();

    return schemaReader.Read(schemaString);
  }

  /// <inheritdoc/>
  public Schema GetSchema(string subject, int version)
  {
    string schemaString = GetSchemaString(subject, version);

    var schemaReader = new JsonSchemaReader();

    return schemaReader.Read(schemaString);
  }

  /// <summary>
  /// Gets the schema as a string from the file system.
  /// </summary>
  /// <param name="subject"></param>
  /// <param name="version"></param>
  /// <returns></returns>
  string GetSchemaString(string subject, int version)
  {
    string schemaFileName = $"{subject}-v{version}.avsc";

    string? schemaFile = Directory.GetFiles(_options.Path, schemaFileName).FirstOrDefault();

    return string.IsNullOrEmpty(schemaFile)
        ? throw new FileNotFoundException($"Schema file {schemaFileName} in path {_options.Path} not found.")
        : File.ReadAllText(schemaFile);
  }

  /// <summary>
  /// Gets the schema as a string from the file system asynchronously.
  /// </summary>
  /// <param name="subject"></param>
  /// <param name="version"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  async Task<string> GetSchemaStringAsync(string subject, int version, CancellationToken cancellationToken)
  {
    string schemaFileName = $"{subject}-v{version}.avsc";

    string? schemaFile = Directory.GetFiles(_options.Path, schemaFileName).FirstOrDefault();

    return string.IsNullOrEmpty(schemaFile)
        ? throw new FileNotFoundException($"Schema file {schemaFileName} in path {_options.Path} not found.")
        : await File.ReadAllTextAsync(schemaFile, cancellationToken);
  }
}
