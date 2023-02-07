using Devantler.DataMesh.DataProduct.Configuration.Options;

namespace Devantler.DataMesh.DataProduct;

/// <summary>
/// Extensions to more easily interact with options and feature flags from the configuration.
/// </summary>
public static class FeatureManagementExtensions
{
    /// <summary>
    /// Retrieves the value for a feature flag key.
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="featureName"></param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="configuration"/> is null.</exception>
    public static string GetFeatureValue(this IConfiguration configuration, string featureName)
    {
        return configuration == null
          ? throw new ArgumentNullException(nameof(configuration))
          : configuration.GetSection(FeatureFlagsOptions.Key).GetValue<string>(featureName)
            ?.ToLower(System.Globalization.CultureInfo.InvariantCulture) ?? string.Empty;
    }

    /// <summary>
    /// Checks whether a feature is enabled or not.
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="featureFlag"></param>
    public static bool IsFeatureEnabled(this IConfiguration configuration, string featureFlag) =>
      configuration.GetValue<bool>($"{FeatureFlagsOptions.Key}:{featureFlag}");

    /// <summary>
    /// Checks whether a feature is enabled or not.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="configuration"></param>
    /// <param name="featureFlag"></param>
    /// <param name="value"></param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="configuration"/> is null.</exception>
    public static bool IsFeatureEnabled<T>(this IConfiguration configuration, string featureFlag, object value)
    {
        if (configuration == null)
            throw new ArgumentNullException(nameof(configuration));

        var featureValue = configuration.GetSection($"{FeatureFlagsOptions.Key}:{featureFlag}").Get<T>();
        return featureValue switch
        {
            bool boolValue => boolValue == (value as bool?),
            int intValue => intValue == (value as int?),
            string stringValue => stringValue == (value as string),
            string[] stringArrayValue => stringArrayValue.Contains(value as string),
            _ => false,
        };
    }
}
