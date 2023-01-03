using Confluent.SchemaRegistry;

namespace Devantler.DataMesh.SchemaRegistry.Providers.Kafka;

public class KafkaSchemaRegistryService : ISchemaRegistryService
{
    private readonly KafkaSchemaRegistryOptions _schemaRegistryOptions;

    public KafkaSchemaRegistryService(KafkaSchemaRegistryOptions schemaRegistryOptions)
    {
        _schemaRegistryOptions = schemaRegistryOptions;
    }

    public async Task<Avro.Schema> GetSchemaAsync(string subject, int version)
    {
        var cachedSchemaRegistryClient = new CachedSchemaRegistryClient(new SchemaRegistryConfig { Url = _schemaRegistryOptions.Url });
        var registeredSchemas = new List<RegisteredSchema>
        {
            await cachedSchemaRegistryClient.GetRegisteredSchemaAsync(subject, version)
        };
        return Avro.Schema.Parse(registeredSchemas[0].SchemaString);
    }
}
