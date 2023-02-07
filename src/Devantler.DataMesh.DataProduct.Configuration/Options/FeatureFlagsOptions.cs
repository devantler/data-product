namespace Devantler.DataMesh.DataProduct.Configuration.Options;

/// <summary>
/// Options to configure the features in the date product.
/// </summary>
public class FeatureFlagsOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the features options.
    /// </summary>
    public const string Key = "DataProduct:FeatureFlags";

    /// <summary>
    /// Whether a data store should be enabled for the data product.
    /// </summary>
    public bool EnableDataStore { get; set; }
    /// <summary>
    /// A list of APIs that should be enabled for the data product.
    /// </summary>
    public List<string> EnableApis { get; set; } = new();

    /// <summary>
    /// Whether a client-side cache should be enabled for the data product.
    /// </summary>
    public bool EnableCaching { get; set; }

    /// <summary>
    /// Whether metadata should be collected and made observable from a metadata platform.
    /// </summary>
    public bool EnableMetadataPlatform { get; set; }

    /// <summary>
    /// Whether authentication is required to access the data product.
    /// </summary>
    public bool EnableAuthentication { get; set; }

    /// <summary>
    /// Whether authorisation is required to access the data product.
    /// </summary>
    public bool EnableAuthorisation { get; set; }

    /// <summary>
    /// Whether metrics should be collected and made observable from a metrics platform.
    /// </summary>
    public bool EnableMetrics { get; set; }

    /// <summary>
    /// Whether tracing should be collected and made observable from a tracing platform.
    /// </summary>
    public bool EnableTracing { get; set; }

    /// <summary>
    /// Whether logging should be enabled and made observable from a logging platform.
    /// </summary>
    public bool EnableLogging { get; set; }

    /// <summary>
    /// Whether health data should be collected and made observable from a liveability platform.
    /// </summary>
    public bool EnableHealthChecks { get; set; }

    /// <summary>
    /// Whether the data product should be able to publish and subscribe to events.
    /// </summary>
    public bool EnablePubSub { get; set; }

    /// <summary>
    /// Whether the data product should be made triggerable by bindings.
    /// </summary>
    public bool EnableBindings { get; set; }
}
