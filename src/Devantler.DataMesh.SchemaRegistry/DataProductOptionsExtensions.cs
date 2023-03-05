using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistryOptions;
using Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistryOptions.Providers;

namespace Devantler.DataMesh.SchemaRegistry;

/// <summary>
/// Gets the schema
/// </summary>
public static class DataProductOptionsExtensions
{
    /// <summary>
    /// Gets the requested schema registry type.
    /// </summary>
    /// <param name="options"></param>
    public static ISchemaRegistryService GetSchemaRegistryService(this DataProductOptions options)
    {
        var schemaRegistryType = options.SchemaRegistry.Type;

        return schemaRegistryType switch
        {
            SchemaRegistryType.Local => new LocalSchemaRegistryService(options.SchemaRegistry as LocalSchemaRegistryOptions),
            SchemaRegistryType.Kafka => new KafkaSchemaRegistryService(options.SchemaRegistry as KafkaSchemaRegistryOptions),
            _ => throw new NotImplementedException($"The schema registry type '{schemaRegistryType}' is not implemented yet")
        };
    }
}
