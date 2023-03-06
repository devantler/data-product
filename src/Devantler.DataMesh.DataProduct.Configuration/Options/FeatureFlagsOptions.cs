using Devantler.DataMesh.DataProduct.Apis.GraphQL;

namespace Devantler.DataMesh.DataProduct.Configuration.Options;

/// <summary>
/// Options to configure the features in the date product.
/// </summary>
public class FeatureFlagsOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the features options.
    /// </summary>
    public const string Key = "DataProduct:FeatureFlags";

    /// <summary>
    /// A list of APIs that should be enabled for the data product.
    /// </summary>
    public List<ApiFeatureFlagValues> EnableApis { get; set; } = new();

    /// <summary>
    /// A flag to indicate if data sources should be enabled for the data product.
    /// </summary>
    public bool EnableDataSources { get; set; }
}
