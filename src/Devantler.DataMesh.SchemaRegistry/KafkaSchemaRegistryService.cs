using Chr.Avro.Representation;
using Confluent.SchemaRegistry;
using Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistryOptions.Providers;

namespace Devantler.DataMesh.SchemaRegistry;

/// <summary>
/// A Kafka schema registry service.
/// </summary>
public class KafkaSchemaRegistryService : ISchemaRegistryService
{
    readonly KafkaSchemaRegistryOptions? _schemaRegistryOptions;

    /// <summary>
    /// A constructor to construct a kafka schema registry service.
    /// </summary>
    /// <param name="schemaRegistryOptions"></param>
    public KafkaSchemaRegistryService(KafkaSchemaRegistryOptions? schemaRegistryOptions) => _schemaRegistryOptions = schemaRegistryOptions;

    /// <inheritdoc/>
    public async Task<Chr.Avro.Abstract.Schema> GetSchemaAsync(string subject, int version)
    {
        CachedSchemaRegistryClient cachedSchemaRegistryClient = new(new SchemaRegistryConfig { Url = _schemaRegistryOptions?.Url });
        List<RegisteredSchema> registeredSchemas = new()
        {
            await cachedSchemaRegistryClient.GetRegisteredSchemaAsync(subject, version)
        };
        var schemaReader = new JsonSchemaReader();
        return schemaReader.Read(registeredSchemas[0].SchemaString);
    }
}
