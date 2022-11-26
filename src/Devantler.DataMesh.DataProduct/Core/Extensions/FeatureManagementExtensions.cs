namespace Devantler.DataMesh.DataProduct.Core.Extensions;

public static class FeatureManagementExtensions
{
    public static bool IsFeatureEnabled(this IConfiguration configuration, string featureFlag)
    {
        return configuration.GetValue<bool>($"Features:{featureFlag}");
    }
}
