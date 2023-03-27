namespace Devantler.DataMesh.DataProduct.Configuration.Options.DataStore;

/// <summary>
/// Options to configure a data store.
/// </summary>
public class DataStoreOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the data store options.
    /// </summary>
    public const string Key = "DataStore";

    /// <summary>
    /// The data store type to use.
    /// </summary>
    public virtual DataStoreType Type { get; set; }

    /// <summary>
    /// The data store provider to use.
    /// </summary>
    public virtual string Provider { get; set; } = string.Empty;

    /// <summary>
    /// The connection string to the data store.
    /// </summary>
    public virtual string ConnectionString { get; set; } = string.Empty;
}
