namespace Devantler.DataMesh.DataProduct.Extensions;

/// <summary>
/// Extension methods for <see cref="IConfiguration"/>.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Adds a data product configuration from command line arguments, environment variables, config.{environment}.{json|yml|yaml}, and config.{json|yml|yaml}.
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