namespace Devantler.DataProduct.Core.Configuration.Options.SchemaRegistry.Providers;

/// <summary>
/// Options to configure a Kafka schema registry used by the data product.
/// </summary>
public class KafkaSchemaRegistryOptions : SchemaRegistryOptions
{
    /// <inheritdoc/>
    public override SchemaRegistryType Type { get; set; } = SchemaRegistryType.Kafka;

    /// <inheritdoc/>
    public override string Url { get; set; } = string.Empty;
}