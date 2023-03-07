using Devantler.DataMesh.DataProduct.Configuration.Options.Services.SchemaRegistry;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.SchemaRegistry.Providers;

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
    public static ISchemaRegistryService CreateSchemaRegistryService(this ISchemaRegistryOptions options)
    {
        var schemaRegistryType = options.Type;

        return schemaRegistryType switch
        {
            SchemaRegistryType.Local => new LocalSchemaRegistryService(options as LocalSchemaRegistryOptions),
            SchemaRegistryType.Kafka => new KafkaSchemaRegistryService(options as KafkaSchemaRegistryOptions),
            _ => throw new NotImplementedException($"The schema registry type '{schemaRegistryType}' is not implemented yet")
        };
    }
}
