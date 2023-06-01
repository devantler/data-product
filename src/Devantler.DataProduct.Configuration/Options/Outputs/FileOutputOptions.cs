namespace Devantler.DataProduct.Configuration.Options.Outputs;
/// <summary>
/// Options to configure a local output for the data product.
/// </summary>
public class FileOutputOptions : OutputOptions
{
    /// <inheritdoc/>
    public override OutputType Type { get; set; } = OutputType.File;
    /// <summary>
    /// The path to the file to read from.
    /// </summary>
    public string FilePath { get; set; } = string.Empty;
}
