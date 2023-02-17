namespace Devantler.DataMesh.DataProduct.Configuration.Options.DataStoreOptions;

/// <summary>
/// Options to configure a data store.
/// </summary>
public interface IDataStoreOptions
{
    /// <summary>
    /// The data store type to use.
    /// </summary>
    public DataStoreType Type { get; set; }

    /// <summary>
    /// The connection string to the data store.
    /// </summary>
    public string ConnectionString { get; set; }
}
