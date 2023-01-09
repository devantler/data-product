namespace Devantler.DataMesh.DataProduct.Configuration;

/// <summary>
/// Options to configure a date product.
/// </summary>
public class DataProductOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the data product options.
    /// </summary>
    public const string Key = "DataProduct";

    /// <summary>
    /// The name of the data product.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// A description of the data product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The version of the data product.
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Options for the owner of the data product.
    /// </summary>
    public OwnerOptions Owner { get; set; } = new();

    /// <summary>
    /// Options for the features in the data product.
    /// </summary>
    public FeaturesOptions Features { get; set; } = new();

    /// <summary>
    /// Options for the schema used in the data product.
    /// </summary>
    public SchemaOptions Schema { get; set; } = new();

    /// <summary>
    /// Options for the schema registry used by the data product.
    /// </summary>
    public SchemaRegistryOptions SchemaRegistry { get; set; } = new();
}
