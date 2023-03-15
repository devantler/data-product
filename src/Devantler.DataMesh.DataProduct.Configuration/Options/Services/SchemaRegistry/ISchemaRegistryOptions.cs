namespace Devantler.DataMesh.DataProduct.Configuration.Options.Services.SchemaRegistry;

/// <summary>
/// Options to configure a schema registry used by the data product.
/// </summary>
public interface ISchemaRegistryOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the schema registry options.
    /// </summary>
    public const string Key = "Services:SchemaRegistry";

    /// <summary>
    /// The schema registry type to use.
    /// </summary>
    public SchemaRegistryType Type { get; set; }

    /// <summary>
    /// Options for the schema used by the data product.
    /// </summary>
    public SchemaOptions Schema { get; set; }
}
