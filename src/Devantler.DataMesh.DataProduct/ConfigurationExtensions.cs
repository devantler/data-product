namespace Devantler.DataMesh.DataProduct;

/// <summary>
/// Extension methods for <see cref="IConfiguration"/>.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Adds configuration from config.json, config.yml, config.yaml, config.{environment}.json, config.{environment}.yml, config.{environment}.yaml, environment variables, and command line arguments.
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="environment"></param>
    /// <param name="args"></param>
    public static ConfigurationManager AddDataProductConfiguration(this ConfigurationManager configuration, IWebHostEnvironment environment, string[] args)
    {
        string ymlOrJsonFileExtension = File.Exists("config.yml") ? "yml" : "json";
        string fileExtension = File.Exists("config.yaml") ? "yaml" : ymlOrJsonFileExtension;

        _ = fileExtension.Equals("json", StringComparison.Ordinal)
            ? configuration.AddJsonFile($"config.{fileExtension}", optional: false)
                .AddJsonFile($"config.{environment.EnvironmentName}.{fileExtension}", optional: true)
            : configuration.AddYamlFile($"config.{fileExtension}", optional: false)
                .AddYamlFile($"config.{environment.EnvironmentName}.{fileExtension}", optional: true);

        _ = configuration.AddEnvironmentVariables()
            .AddCommandLine(args);

        return configuration;
    }
}