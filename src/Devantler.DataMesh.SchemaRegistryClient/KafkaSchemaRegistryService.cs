using Chr.Avro.Representation;
using Confluent.SchemaRegistry;
using Devantler.DataMesh.SchemaRegistryClient.Models;

namespace Devantler.DataMesh.SchemaRegistryClient;

/// <summary>
/// A Kafka schema registry service.
/// </summary>
public class KafkaSchemaRegistryService : ISchemaRegistryService
{
    readonly KafkaSchemaRegistryOptions _options;

    /// <summary>
    /// A constructor to construct a kafka schema registry service.
    /// </summary>
    /// <param name="options"></param>
    public KafkaSchemaRegistryService(Action<KafkaSchemaRegistryOptions> options)
    {
        _options = new KafkaSchemaRegistryOptions();
        options(_options);
    }

    /// <summary>
    /// A constructor to construct a kafka schema registry service.
    /// </summary>
    public KafkaSchemaRegistryService(KafkaSchemaRegistryOptions options)
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
        var schemaReader = new JsonSchemaReader();
        return schemaReader.Read(registeredSchemas[0].SchemaString);
    }
}
