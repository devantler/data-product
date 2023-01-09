namespace Devantler.DataMesh.DataProduct.Configuration;

/// <summary>
/// Options to configure the schema registry used in the date product.
/// </summary>
public class SchemaRegistryOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the schema registry options.
    /// </summary>
    public const string Key = "DataProduct:SchemaRegistry";

    /// <summary>
    /// The schema registry type to use.
    /// </summary>
    public SchemaRegistryType Type { get; set; }

    /// <summary>
    /// The path to the schema registry if the Local schema registry type is selected.
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// The URL to the schema registry if the Kafka schema registry type is selected.
    /// </summary>
    public string? Url { get; set; }
}

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
