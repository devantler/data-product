namespace Devantler.DataMesh.SchemaRegistry.Providers.Local;

/// <summary>
/// Options to configure a Local schema registry.
/// </summary>
public class LocalSchemaRegistryOptions
{
    /// <summary>
    /// The path to the local schema registry.
    /// </summary>
    public string Path { get; set; } = null!;
}
