namespace Devantler.DataMesh.DataProduct.Configuration;

/// <summary>
/// Options to configure the features in the date product.
/// </summary>
public class FeaturesOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the features options.
    /// </summary>
    public const string Key = "DataProduct:Features";

    /// <summary>
    /// A list of APIs that should be enabled for the data product.
    /// </summary>
    public List<string> Apis { get; set; } = new();

    /// <summary>
    /// Whether a client-side cache should be enabled for the data product.
    /// </summary>
    public bool Caching { get; set; }

    /// <summary>
    /// Whether metadata should be collected and made observable from a metadata platform.
    /// </summary>
    public bool Metadata { get; set; }

    /// <summary>
    /// Whether authentication is required to access the data product.
    /// </summary>
    public bool Authentication { get; set; }

    /// <summary>
    /// Whether authorisation is required to access the data product.
    /// </summary>
    public bool Authorisation { get; set; }

    /// <summary>
    /// Whether metrics should be collected and made observable from a metrics platform.
    /// </summary>
    public bool Metrics { get; set; }

    /// <summary>
    /// Whether tracing should be collected and made observable from a tracing platform.
    /// </summary>
    public bool Tracing { get; set; }

    /// <summary>
    /// Whether logging should be enabled and made observable from a logging platform.
    /// </summary>
    public bool Logging { get; set; }

    /// <summary>
    /// Whether health data should be collected and made observable from a liveability platform.
    /// </summary>
    public bool Health { get; set; }
}
