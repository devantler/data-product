namespace Devantler.DataProduct.Configuration.Options.Outputs;

/// <summary>
/// Options to configure a output for the data product.
/// </summary>
public class OutputOptions
{
    /// <summary>
    /// The key for the output options.
    /// </summary>
    public const string Key = "Outputs";

    /// <summary>
    /// The output type to use.
    /// </summary>
    public virtual OutputType Type { get; set; }
}
