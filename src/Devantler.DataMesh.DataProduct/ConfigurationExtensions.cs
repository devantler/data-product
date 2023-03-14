namespace Devantler.DataMesh.DataProduct;

/// <summary>
/// Extension methods for <see cref="IConfiguration"/>.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Adds configuration from dp-config.json, dp-config.yml, dp-config.yaml, dp-config.{environment}.json, dp-config.{environment}.yml, dp-config.{environment}.yaml, environment variables, and command line arguments.
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="environment"></param>
    /// <param name="args"></param>
    public static ConfigurationManager AddDataProductConfiguration(this ConfigurationManager configuration, IWebHostEnvironment environment, string[] args)
    {
        string ymlOrJsonFileExtension = File.Exists("dp-config.yml") ? "yml" : "json";
        string fileExtension = File.Exists("dp-config.yaml") ? "yaml" : ymlOrJsonFileExtension;

        _ = fileExtension.Equals("json", StringComparison.Ordinal)
            ? configuration.AddJsonFile($"dp-config.{fileExtension}", optional: false)
                .AddJsonFile($"dp-config.{environment.EnvironmentName}.{fileExtension}", optional: true)
            : configuration.AddYamlFile($"dp-config.{fileExtension}", optional: false)
                .AddYamlFile($"dp-config.{environment.EnvironmentName}.{fileExtension}", optional: true);

        _ = configuration.AddEnvironmentVariables()
            .AddCommandLine(args);

        return configuration;
    }
}