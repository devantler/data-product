namespace Devantler.DataMesh.SchemaRegistry.Providers.Kafka;

/// <summary>
/// Options to configure a Kafka schema registry.
/// </summary>
public class KafkaSchemaRegistryOptions
{
    /// <summary>
    /// The Url of the Kafka schema registry.
    /// </summary>
    public string? Url { get; set; }
}
