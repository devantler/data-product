namespace Devantler.DataMesh.DataProduct.Configuration.SchemaRegistry;

/// <summary>
/// Options to configure a schema registry used by the data product.
/// </summary>
public interface ISchemaRegistryOptions
{
    /// <summary>
    /// The schema registry type to use.
    /// </summary>
    public abstract SchemaRegistryType Type { get; set; }
}
