namespace Devantler.DataMesh.DataProduct.Configuration;

/// <summary>
/// Options to configure the owner of the date product.
/// </summary>
public class OwnerOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the owner options.
    /// </summary>
    public const string Key = "DataProduct:Owner";

    /// <summary>
    /// The name of the owner.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The owner's email.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The organisation that the owner belongs to.
    /// </summary>
    public string Organization { get; set; } = string.Empty;

    /// <summary>
    /// The owner's phone number..
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// The owner's website.
    /// </summary>
    public string Website { get; set; } = string.Empty;
}
