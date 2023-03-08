using Devantler.DataMesh.DataProduct.Configuration.Options;

namespace Devantler.DataMesh.DataProduct.Features;

/// <summary>
/// Extensions to FeatureManagement, to allow reading feature flags during startup.
/// </summary>
public static class FeatureManagementExtensions
{
    /// <summary>
    /// Configures the web application to use a feature if a complex feature flag is enabled.
    /// </summary>
    public static WebApplication UseForFeature(this WebApplication app, string featureFlagName, string featureFlagValue, Action<WebApplication> configure)
    {
        if (app.Configuration.GetSection(FeatureFlagsOptions.Key).GetValue<List<string>>(featureFlagName)?.Contains(featureFlagValue) == true)
        {
            configure(app);
        }
        return app;
    }
}