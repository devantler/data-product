namespace Devantler.DataProduct.Configuration.Options.FeatureFlags;

/// <summary>
/// Options to configure the features in the date product.
/// </summary>
public class FeatureFlagsOptions
{
  /// <summary>
  /// A key to indicate the section in the configuration file that contains the features options.
  /// </summary>
  public const string Key = "FeatureFlags";

  /// <summary>
  /// A list of APIs that should be enabled for the data product.
  /// </summary>
  public List<ApiFeatureFlagValues> EnableApis { get; set; } = new() { ApiFeatureFlagValues.Rest, ApiFeatureFlagValues.GraphQL };

  /// <summary>
  /// A flag to indicate if a dashboard should be enabled for the data product.
  /// </summary>
  public bool EnableDashboard { get; set; } = true;

  /// <summary>
  /// A flag to indicate if inputs should be enabled for the data product.
  /// </summary>
  public bool EnableInputs { get; set; }

  /// <summary>
  /// A flag to indicate if outputs should be enabled for the data product.
  /// </summary>
  public bool EnableOutputs { get; set; }

  /// <summary>
  /// A flag to indicate if a data catalog integration should be enabled for the data product.
  /// </summary>
  public bool EnableDataCatalog { get; set; }

  /// <summary>
  /// A flag to indicate if caching should be enabled for the data product.
  /// </summary>
  public bool EnableCaching { get; set; } = true;

  /// <summary>
  /// A flag to indicate if authentication and authorization should be enabled for the data product.
  /// </summary>
  public bool EnableAuth { get; set; } = false;

  /// <summary>
  /// A flag to indicate if webhooks should be enabled for the data product.
  /// </summary>
  public bool EnableWebhooks { get; set; }

  /// <summary>
  /// A flag to indicate if telemetry should be enabled for the data product.
  /// </summary>
  public bool EnableTelemetry { get; set; }
}
