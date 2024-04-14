namespace Devantler.DataProduct.Configuration.Options.SchemaRegistry.Providers;

/// <summary>
/// Options to configure a local schema registry used by the data product.
/// </summary>
public class LocalSchemaRegistryOptions : SchemaRegistryOptions
{
  /// <inheritdoc/>
  public override SchemaRegistryType Type { get; set; } = SchemaRegistryType.Local;

  /// <inheritdoc/>
  public override string Url { get; set; } = "assets/schemas";
}
