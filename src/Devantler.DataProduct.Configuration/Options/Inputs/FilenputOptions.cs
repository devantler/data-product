namespace Devantler.DataProduct.Configuration.Options.Inputs;
/// <summary>
/// Options to configure a local input for the data product.
/// </summary>
public class FileInputOptions : InputOptions
{
  /// <inheritdoc/>
  public override InputType Type { get; set; } = InputType.File;
  /// <summary>
  /// The path to the file to read from.
  /// </summary>
  public string FilePath { get; set; } = string.Empty;
}
