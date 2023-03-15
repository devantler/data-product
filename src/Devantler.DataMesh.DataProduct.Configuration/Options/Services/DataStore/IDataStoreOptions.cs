namespace Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataStore;

/// <summary>
/// Options to configure a data store.
/// </summary>
public interface IDataStoreOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the data store options.
    /// </summary>
    public const string Key = "Services:DataStore";
    /// <summary>
    /// The data store type to use.
    /// </summary>
    public DataStoreType Type { get; set; }

    /// <summary>
    /// The data store provider to use.
    /// </summary>
    public string Provider { get; set; }

    /// <summary>
    /// The connection string to the data store.
    /// </summary>
    public string ConnectionString { get; set; }
}
