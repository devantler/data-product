namespace Devantler.DataProduct.Configuration.Options.SchemaRegistry;

/// <summary>
/// Options to configure a schema registry for the data product.
/// </summary>
public class SchemaRegistryOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the schema registry options.
    /// </summary>
    public const string Key = "SchemaRegistry";

    /// <summary>
    /// The schema registry type to use.
    /// </summary>
    public virtual SchemaRegistryType Type { get; set; }

    /// <summary>
    /// The URL to the schema registry. If the schema registry is local, this will be the path to folder containing the schemas.
    /// </summary>
    public virtual string Url { get; set; } = string.Empty;

    /// <summary>
    /// Options for the schema used by the data product.
    /// </summary>
    public SchemaOptions Schema { get; set; } = new();
}
