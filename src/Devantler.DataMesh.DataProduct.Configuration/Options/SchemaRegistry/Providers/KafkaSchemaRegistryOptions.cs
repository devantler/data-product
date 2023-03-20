namespace Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistry.Providers;

/// <summary>
/// Options to configure a Kafka schema registry used by the data product.
/// </summary>
public class KafkaSchemaRegistryOptions : ISchemaRegistryOptions
{
    /// <inheritdoc/>
    public string Name { get; set; } = "Kafka";

    /// <inheritdoc/>
    public SchemaRegistryType Type { get; set; } = SchemaRegistryType.Kafka;

    /// <summary>
    /// The URL to the Kafka schema registry.
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <Inheritdoc/>
    public SchemaOptions Schema { get; set; } = new();
}