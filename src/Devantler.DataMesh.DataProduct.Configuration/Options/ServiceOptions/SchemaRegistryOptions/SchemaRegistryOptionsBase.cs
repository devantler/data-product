namespace Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.SchemaRegistryOptions;

/// <summary>
/// Options to configure a schema registry used by the data product.
/// </summary>
public abstract class SchemaRegistryOptionsBase : ISchemaRegistryOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the schema registry options.
    /// </summary>
    public const string Key = "DataProduct:Services:SchemaRegistry";

    /// <inheritdoc/>
    public abstract SchemaRegistryType Type { get; set; }
    /// <inheritdoc/>
    public SchemaOptions Schema { get; set; } = new();
}
