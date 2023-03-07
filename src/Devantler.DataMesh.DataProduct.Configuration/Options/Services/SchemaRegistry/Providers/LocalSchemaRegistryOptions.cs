namespace Devantler.DataMesh.DataProduct.Configuration.Options.Services.SchemaRegistry.Providers;

/// <summary>
/// Options to configure a local schema registry used by the data product.
/// </summary>
public class LocalSchemaRegistryOptions : ISchemaRegistryOptions
{
    /// <inheritdoc/>
    public SchemaRegistryType Type { get; set; } = SchemaRegistryType.Local;

    /// <summary>
    /// The path to the local schema registry.
    /// </summary>
    public string Path { get; set; } = "schemas";

    /// <inheritdoc/>
    public SchemaOptions Schema { get; set; } = new();
}
