namespace Devantler.DataProduct.Core.Configuration.Options;

/// <summary>
/// Options to configure the license used by the date product.
/// </summary>
public class LicenseOptions
{
    /// <summary>
    /// The name of the license.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// A URL that points to the license.
    /// </summary>
    public string Url { get; set; } = string.Empty;
}