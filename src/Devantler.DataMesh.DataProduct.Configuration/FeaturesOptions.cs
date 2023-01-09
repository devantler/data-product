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
    /// The type of the data store that should be used by the data product.
    /// </summary>
    public DataStoreType DataStoreType { get; set; }

    /// <summary>
    /// The specific provider that should be used for the chosen data store type.
    /// </summary>
    public DataStoreProvider DataStoreProvider { get; set; } = DataStoreProvider.Auto;

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

/// <summary>
/// Supported data store types.
/// </summary>
public enum DataStoreType
{
    /// <summary>
    /// A relational data store.
    /// </summary>
    Relational,

    /// <summary>
    /// A document-based data store.
    /// </summary>
    Document,

    /// <summary>
    /// A graph-based data store.
    /// </summary>
    Graph
}

/// <summary>
/// Supported data store providers for the different data store types.
/// </summary>
public enum DataStoreProvider
{
    /// <summary>
    /// Automatically decide the which data store provider to use for the specified data store type.
    /// </summary>
    Auto,

    /// <summary>
    /// SQLite a relational data store provider.
    /// </summary>
    Sqlite,

    /// <summary>
    /// MongoDb a document-based data store provider
    /// </summary>
    MongoDb,

    /// <summary>
    /// Neo4J a graph-based data store provider.
    /// </summary>
    Neo4J,
}
