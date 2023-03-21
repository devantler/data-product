namespace Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistry.Providers;

/// <summary>
/// Options to configure a local schema registry used by the data product.
/// </summary>
public class LocalSchemaRegistryOptions : ISchemaRegistryOptions
{
    /// <inheritdoc/>
    public SchemaRegistryType Type { get; set; } = SchemaRegistryType.Local;

    /// <inheritdoc/>
    public string Url { get; set; } = "schemas";

    /// <inheritdoc/>
    public SchemaOptions Schema { get; set; } = new();
}
