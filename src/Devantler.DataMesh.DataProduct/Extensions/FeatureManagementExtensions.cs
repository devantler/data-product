namespace Devantler.DataMesh.DataProduct.Extensions;

/// <summary>
/// Extensions to more easily interact with feature configurations from feature flags.
/// </summary>
public static class FeatureManagementExtensions
{
    /// <summary>
    /// Retrieves the value for a feature flag key.
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="featureName"></param>
    /// <returns></returns>
    public static string GetFeatureValue(this IConfiguration configuration, string featureName)
    {
        return configuration == null
            ? throw new ArgumentNullException(nameof(configuration))
            : configuration.GetSection("Features").GetValue<string>(featureName) ?? string.Empty;
    }

    /// <summary>
    /// Checks whether a feature is enabled or not.
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="featureFlag"></param>
    /// <returns></returns>
    public static bool IsFeatureEnabled(this IConfiguration configuration, string featureFlag) =>
        configuration.GetValue<bool>($"Features:{featureFlag}");

    /// <summary>
    /// Checks whether a feature is enabled or not.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="configuration"></param>
    /// <param name="featureFlag"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsFeatureEnabled<T>(this IConfiguration configuration, string featureFlag, object value)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        T? featureValue = configuration.GetSection($"Features:{featureFlag}").Get<T>();
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
