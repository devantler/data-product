namespace Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.DataStoreOptions.Relational;

/// <summary>
/// Options to configure a relational data store.
/// </summary>
public abstract class RelationalDataStoreOptionsBase : DataStoreOptionsBase
{
    /// <inheritdoc/>
    public override DataStoreType Type { get; set; } = DataStoreType.Relational;
}
