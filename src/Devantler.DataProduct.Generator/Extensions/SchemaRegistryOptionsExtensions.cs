using Devantler.DataProduct.Core.Configuration.Options.SchemaRegistry;
using Devantler.SchemaRegistry.Client;

namespace Devantler.DataProduct.Generator.Extensions;

/// <summary>
/// Extensions for <see cref="SchemaRegistryOptions"/>.
/// </summary>
public static class SchemaRegistryOptionsExtensions
{
    /// <summary>
    /// Creates a schema registry service based on the options.
    /// </summary>
    /// <param name="options">The options to use.</param>
    /// <returns>A schema registry service.</returns>
    public static ISchemaRegistryClient CreateSchemaRegistryClient(this SchemaRegistryOptions options)
    {
        return options.Type switch
        {
            SchemaRegistryType.Kafka => new KafkaSchemaRegistryClient(
                o => o.Url = options.Url
            ),
            SchemaRegistryType.Local => new LocalSchemaRegistryClient(
                o => o.Path = options.Url
            ),
            _ => throw new NotSupportedException($"Schema registry type '{options.Type}' is not supported.")
        };
    }
}