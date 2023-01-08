namespace Devantler.DataMesh.DataProduct.Extensions;

public static class FeatureManagementExtensions
{
    public static string GetFeatureValue(this IConfiguration configuration, string featureName)
    {
        return configuration == null
            ? throw new ArgumentNullException(nameof(configuration))
            : configuration.GetSection("Features").GetValue<string>(featureName) ?? string.Empty;
    }

    public static bool IsFeatureEnabled(this IConfiguration configuration, string featureFlag) =>
        configuration.GetValue<bool>($"Features:{featureFlag}");

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
