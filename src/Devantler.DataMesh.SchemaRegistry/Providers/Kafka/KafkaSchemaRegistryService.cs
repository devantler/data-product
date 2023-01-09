using Confluent.SchemaRegistry;

namespace Devantler.DataMesh.SchemaRegistry.Providers.Kafka;

/// <summary>
/// A Kafka schema registry service.
/// </summary>
public class KafkaSchemaRegistryService : ISchemaRegistryService
{
    readonly KafkaSchemaRegistryOptions _schemaRegistryOptions;

    /// <summary>
    /// A constructor to construct a kafka schema registry service.
    /// </summary>
    /// <param name="schemaRegistryOptions"></param>
    public KafkaSchemaRegistryService(KafkaSchemaRegistryOptions schemaRegistryOptions) => _schemaRegistryOptions = schemaRegistryOptions;

    /// <inheritdoc/>
    public async Task<Avro.Schema> GetSchemaAsync(string subject, int version)
    {
        CachedSchemaRegistryClient cachedSchemaRegistryClient = new(new SchemaRegistryConfig { Url = _schemaRegistryOptions.Url });
        List<RegisteredSchema> registeredSchemas = new()
        {
            await cachedSchemaRegistryClient.GetRegisteredSchemaAsync(subject, version)
        };
        return Avro.Schema.Parse(registeredSchemas[0].SchemaString);
    }
}
