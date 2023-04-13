namespace Devantler.DataProduct.Features.Configuration.Extensions;

/// <summary>
/// Extension methods for <see cref="ConfigurationManager"/>.
/// </summary>
public static class ConfigurationManagerExtensions
{
    /// <summary>
    /// Adds a data product configuration from command line arguments, environment variables, config.{environment}.{json|yml|yaml}, and config.{json|yml|yaml}.
    /// </summary>
    /// <param name="configurationManager"></param>
    /// <param name="environment"></param>
    /// <param name="args"></param>
    public static ConfigurationManager AddDataProductConfiguration(this ConfigurationManager configurationManager, IWebHostEnvironment environment, string[] args)
    {
        string ymlOrJsonFileExtension = File.Exists("config.yml") ? "yml" : "json";
        string fileExtension = File.Exists("config.yaml") ? "yaml" : ymlOrJsonFileExtension;

        _ = fileExtension.Equals("json", StringComparison.Ordinal)
            ? configurationManager.AddJsonFile($"config.{fileExtension}", optional: true)
                .AddJsonFile($"config.{environment.EnvironmentName}.{fileExtension}", optional: true)
            : configurationManager.AddYamlFile($"config.{fileExtension}", optional: true)
                .AddYamlFile($"config.{environment.EnvironmentName}.{fileExtension}", optional: true);

        _ = configurationManager.AddEnvironmentVariables()
            .AddCommandLine(args);

        return configurationManager;
    }
}