namespace Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.SchemaRegistryOptions.Providers;

/// <summary>
/// Options to configure a Kafka schema registry used by the data product.
/// </summary>
public class KafkaSchemaRegistryOptions : SchemaRegistryOptionsBase
{
    /// <inheritdoc/>
    public override SchemaRegistryType Type { get; set; } = SchemaRegistryType.Kafka;

    /// <summary>
    /// The URL to the Kafka schema registry.
    /// </summary>
    public string? Url { get; set; }
}
