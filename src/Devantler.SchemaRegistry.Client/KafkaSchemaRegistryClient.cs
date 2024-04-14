using Chr.Avro.Representation;
using Confluent.SchemaRegistry;
using Devantler.SchemaRegistry.Client.Models;

namespace Devantler.SchemaRegistry.Client;

/// <summary>
/// A Kafka schema registry client.
/// </summary>
public class KafkaSchemaRegistryClient : ISchemaRegistryClient
{
  readonly KafkaSchemaRegistryOptions _options;

  /// <summary>
  /// A constructor to construct a kafka schema registry client.
  /// </summary>
  /// <param name="options"></param>
  public KafkaSchemaRegistryClient(Action<KafkaSchemaRegistryOptions> options)
  {
    _options = new KafkaSchemaRegistryOptions();
    options(_options);
  }

  /// <summary>
  /// A constructor to construct a kafka schema registry client.
  /// </summary>
  public KafkaSchemaRegistryClient(KafkaSchemaRegistryOptions options)
      => _options = options;

  /// <inheritdoc/>
  public Chr.Avro.Abstract.Schema GetSchema(string subject, int version)
  {
    CachedSchemaRegistryClient cachedSchemaRegistryClient = new(new SchemaRegistryConfig { Url = _options.Url });
    List<RegisteredSchema> registeredSchemas = new()
        {
            cachedSchemaRegistryClient.GetRegisteredSchemaAsync(subject, version).Result
        };
    var schemaReader = new JsonSchemaReader();
    return schemaReader.Read(registeredSchemas[0].SchemaString);
  }

  /// <inheritdoc/>
  public async Task<Chr.Avro.Abstract.Schema> GetSchemaAsync(string subject, int version, CancellationToken cancellationToken = default)
  {
    CachedSchemaRegistryClient cachedSchemaRegistryClient = new(new SchemaRegistryConfig { Url = _options.Url });
    List<RegisteredSchema> registeredSchemas = new()
        {
            await cachedSchemaRegistryClient.GetRegisteredSchemaAsync(subject, version)
        };

    if (registeredSchemas.Count == 0)
      throw new Exception("No schema found for subject: " + subject + " and version: " + version);

    var schemaReader = new JsonSchemaReader();
    return schemaReader.Read(registeredSchemas[0].SchemaString);
  }
}
