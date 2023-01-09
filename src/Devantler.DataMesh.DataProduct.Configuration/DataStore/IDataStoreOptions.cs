namespace Devantler.DataMesh.DataProduct.Configuration.DataStore;

/// <summary>
/// Options to configure a data store.
/// </summary>
public interface IDataStoreOptions
{
    /// <summary>
    /// The data store type to use.
    /// </summary>
    public abstract DataStoreType Type { get; set; }
}
