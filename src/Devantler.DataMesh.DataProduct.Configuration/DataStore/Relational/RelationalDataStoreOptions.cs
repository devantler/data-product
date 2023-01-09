namespace Devantler.DataMesh.DataProduct.Configuration.DataStore.Relational;

/// <summary>
/// Options to configure a relational data store.
/// </summary>
public abstract class RelationalDataStoreOptions : DataStoreOptionsBase
{
    /// <inheritdoc/>
    public override DataStoreType Type { get; set; } = DataStoreType.Relational;

    /// <summary>
    /// The relational data store provider to use.
    /// </summary>
    public RelationalDataStoreProvider Provider { get; set; } = RelationalDataStoreProvider.Auto;
}
