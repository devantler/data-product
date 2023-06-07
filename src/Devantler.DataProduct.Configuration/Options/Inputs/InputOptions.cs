namespace Devantler.DataProduct.Configuration.Options.Inputs;

/// <summary>
/// Options to configure an input for the data product.
/// </summary>
public class InputOptions
{
    /// <summary>
    /// The key for the input options.
    /// </summary>
    public const string Key = "Inputs";

    /// <summary>
    /// The input type to use.
    /// </summary>
    public virtual InputType Type { get; set; }
}
