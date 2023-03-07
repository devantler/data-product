namespace Devantler.DataMesh.DataProduct.Configuration.Options.Services.SchemaRegistry;

/// <summary>
/// Supported schema registries types.
/// </summary>
public enum SchemaRegistryType
{
    /// <summary>
    /// A local schema registry queries and saves schemas on disk.
    /// </summary>
    Local,

    /// <summary>
    /// A Kafka schema registry that queries and saves schemas on a kafka cluster.
    /// </summary>
    Kafka
}
