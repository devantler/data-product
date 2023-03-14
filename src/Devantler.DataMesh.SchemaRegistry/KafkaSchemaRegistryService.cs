using Chr.Avro.Representation;
using Confluent.SchemaRegistry;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.SchemaRegistry.Providers;

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
    public Chr.Avro.Abstract.Schema GetSchema(string subject, int version)
    {
        CachedSchemaRegistryClient cachedSchemaRegistryClient = new(new SchemaRegistryConfig { Url = _schemaRegistryOptions?.Url });
        List<RegisteredSchema> registeredSchemas = new()
        {
            cachedSchemaRegistryClient.GetRegisteredSchemaAsync($"{subject}-value", version).Result
        };
        var schemaReader = new JsonSchemaReader();
        return schemaReader.Read(registeredSchemas[0].SchemaString);
    }

    /// <inheritdoc/>
    public async Task<Chr.Avro.Abstract.Schema> GetSchemaAsync(string subject, int version, CancellationToken cancellationToken = default)
    {
        CachedSchemaRegistryClient cachedSchemaRegistryClient = new(new SchemaRegistryConfig { Url = _schemaRegistryOptions?.Url });
        List<RegisteredSchema> registeredSchemas = new()
        {
            await cachedSchemaRegistryClient.GetRegisteredSchemaAsync($"{subject}-value", version)
        };
        var schemaReader = new JsonSchemaReader();
        return schemaReader.Read(registeredSchemas[0].SchemaString);
    }
}
