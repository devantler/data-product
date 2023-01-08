using Confluent.SchemaRegistry;

namespace Devantler.DataMesh.SchemaRegistry.Providers.Kafka;

public class KafkaSchemaRegistryService : ISchemaRegistryService
{
    readonly KafkaSchemaRegistryOptions _schemaRegistryOptions;

    public KafkaSchemaRegistryService(KafkaSchemaRegistryOptions schemaRegistryOptions) => _schemaRegistryOptions = schemaRegistryOptions;

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
