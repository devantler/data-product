namespace Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistryOptions;

/// <summary>
/// Options to configure a schema registry used by the data product.
/// </summary>
public interface ISchemaRegistryOptions
{
    /// <summary>
    /// The schema registry type to use.
    /// </summary>
    public SchemaRegistryType Type { get; set; }
}
