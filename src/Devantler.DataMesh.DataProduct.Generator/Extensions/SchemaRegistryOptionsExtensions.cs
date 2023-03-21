using Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistry;
using Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistry.Providers;
using Devantler.DataMesh.SchemaRegistry;

namespace Devantler.DataMesh.DataProduct.Generator.Extensions;

/// <summary>
/// Extensions for <see cref="ISchemaRegistryOptions"/>.
/// </summary>
public static class SchemaRegistryOptionsExtensions
{
    /// <summary>
    /// Creates a schema registry service based on the options.
    /// </summary>
    /// <param name="options">The options to use.</param>
    /// <returns>A schema registry service.</returns>
    public static ISchemaRegistryService CreateSchemaRegistryService(this ISchemaRegistryOptions options)
    {
        return options.Type switch
        {
            SchemaRegistryType.Kafka => new KafkaSchemaRegistryService(o =>
            {
                o.Url = options.Url;
            }),
            SchemaRegistryType.Local => new LocalSchemaRegistryService(o =>
            {
                o.Path = options.Url;
            }),
            _ => throw new NotSupportedException($"Schema registry type '{options.Type}' is not supported."),
        };
    }
}