namespace Devantler.SchemaRegistryClient.Models;

/// <summary>
/// Options for a local schema registry.
/// </summary>
public class LocalSchemaRegistryOptions : ISchemaRegistryOptions
{
    /// <summary>
    /// The path to the schema registry.
    /// </summary>
    public string Path { get; set; } = string.Empty;
}
