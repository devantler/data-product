namespace Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.SchemaRegistryOptions.Providers;

/// <summary>
/// Options to configure a local schema registry used by the data product.
/// </summary>
public class LocalSchemaRegistryOptions : SchemaRegistryOptionsBase
{
    /// <inheritdoc/>
    public override SchemaRegistryType Type { get; set; } = SchemaRegistryType.Local;

    /// <summary>
    /// The path to the local schema registry.
    /// </summary>
    public string Path { get; set; } = "Schemas";
}
