using Microsoft.Extensions.Configuration;

namespace Devantler.DataProduct.Configuration.Extensions;

/// <summary>
/// Extension methods for <see cref="ConfigurationManager"/>.
/// </summary>
public static class ConfigurationManagerExtensions
{
  /// <summary>
  /// Adds a data product configuration from command line arguments, environment variables, config.{environment}.{json|yml|yaml}, and config.{json|yml|yaml}.
  /// </summary>
  /// <param name="configurationManager"></param>
  /// <param name="environmentName"></param>
  /// <param name="args"></param>
  public static ConfigurationManager AddDataProductConfiguration(this ConfigurationManager configurationManager, string environmentName, string[] args)
  {
    string ymlOrJsonFileExtension = File.Exists("config.yml") ? "yml" : "json";
    string fileExtension = File.Exists("config.yaml") ? "yaml" : ymlOrJsonFileExtension;

    _ = fileExtension.Equals("json", StringComparison.Ordinal)
        ? configurationManager.AddJsonFile($"config.{fileExtension}", optional: true)
            .AddJsonFile($"config.{environmentName}.{fileExtension}", optional: true)
        : configurationManager.AddYamlFile($"config.{fileExtension}", optional: true)
            .AddYamlFile($"config.{environmentName}.{fileExtension}", optional: true);

    _ = configurationManager.AddEnvironmentVariables()
        .AddCommandLine(args);

    return configurationManager;
  }
}
