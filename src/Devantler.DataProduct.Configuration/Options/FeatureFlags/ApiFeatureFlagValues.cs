namespace Devantler.DataProduct.Configuration.Options.FeatureFlags;

/// <summary>
/// A list of feature flag values for the APIs feature flag.
/// </summary>
public enum ApiFeatureFlagValues
{
    /// <summary>
    /// A value to indicate that the REST API should be enabled.
    /// </summary>
    Rest,
    /// <summary>
    /// A value to indicate that the GraphQL API should be enabled.
    /// </summary>
    GraphQL,
    /// <summary>
    /// A value to indicate that the gRPC API should be enabled.
    /// </summary>
    gRPC
}
