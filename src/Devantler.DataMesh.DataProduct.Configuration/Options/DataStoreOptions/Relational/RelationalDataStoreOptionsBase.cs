namespace Devantler.DataMesh.DataProduct.Configuration.Options.DataStoreOptions.Relational;

/// <summary>
/// Options to configure a relational data store.
/// </summary>
public abstract class RelationalDataStoreOptionsBase : DataStoreOptionsBase
{
    /// <inheritdoc/>
    public override DataStoreType Type { get; set; } = DataStoreType.Relational;

    /// <summary>
    /// The relational data store provider to use.
    /// </summary>
    public abstract RelationalDataStoreProvider Provider { get; set; }
}
