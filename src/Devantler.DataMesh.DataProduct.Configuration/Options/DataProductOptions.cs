using Devantler.DataMesh.DataProduct.Configuration.Options.FeatureFlags;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services;

namespace Devantler.DataMesh.DataProduct.Configuration.Options;

/// <summary>
/// Options to configure a date product.
/// </summary>
public class DataProductOptions
{
    /// <summary>
    /// The name of the data product.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// A description of the data product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The version of the data product.
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Options for the license used by the data product.
    /// </summary>
    public LicenseOptions License { get; set; } = new();

    /// <summary>
    /// Options for the owner of the data product.
    /// </summary>
    public OwnerOptions Owner { get; set; } = new();

    /// <summary>
    /// Options for the features in the data product.
    /// </summary>
    public FeatureFlagsOptions FeatureFlags { get; set; } = new();

    /// <summary>
    /// Options for the services used by or provided by the data product.
    /// </summary>
    public ServicesOptions Services { get; set; } = new();
}
