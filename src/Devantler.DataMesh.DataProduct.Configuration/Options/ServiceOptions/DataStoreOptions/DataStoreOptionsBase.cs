namespace Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.DataStoreOptions;

/// <summary>
/// Options to configure a data store.
/// </summary>
public abstract class DataStoreOptionsBase : IDataStoreOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the data store options.
    /// </summary>
    public const string Key = "DataProduct:Services:DataStore";

    /// <inheritdoc/>
    public abstract DataStoreType Type { get; set; }

    /// <inheritdoc/>
    public abstract string Provider { get; set; }

    /// <inheritdoc/>
    public abstract string ConnectionString { get; set; }
}
