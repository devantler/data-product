namespace Devantler.DataMesh.DataProduct.Configuration;

/// <summary>
/// Options to configure the schema used in the date product.
/// </summary>
public class SchemaOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the schema options.
    /// </summary>
    public const string Key = "DataProduct:Schema";

    /// <summary>
    /// The subject of the schema.
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// The version of the schema.
    /// </summary>
    public int Version { get; set; }
}
