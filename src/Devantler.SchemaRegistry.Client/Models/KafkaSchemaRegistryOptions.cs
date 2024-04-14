namespace Devantler.SchemaRegistry.Client.Models;

/// <summary>
/// Options for a kafka schema registry.
/// </summary>
public class KafkaSchemaRegistryOptions : ISchemaRegistryOptions
{
  /// <summary>
  /// The url of the schema registry.
  /// </summary>
  public string Url { get; set; } = string.Empty;
}
