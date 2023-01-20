namespace Devantler.DataMesh.DataProduct.Configuration.SchemaRegistry;

/// <summary>
/// Supported schema registries types.
/// </summary>
public enum SchemaRegistryType
{
    /// <summary>
    /// A local schema registry queries and saves schemas on disk.
    /// </summary>
    Local = 0,

    /// <summary>
    /// A Kafka schema registry that queries and saves schemas on a kafka cluster.
    /// </summary>
    Kafka = 1
}
