namespace Devantler.DataMesh.DataProduct.Configuration.Options.DataStoreOptions.GraphBased;

/// <summary>
/// Options to configure a graph-based data store.
/// </summary>
public abstract class GraphBasedDataStoreOptionsBase : DataStoreOptionsBase
{
    /// <inheritdoc/>
    public override DataStoreType Type { get; set; } = DataStoreType.GraphBased;

    /// <summary>
    /// The graph-based data store provider to use.
    /// </summary>
    public abstract GraphBasedDataStoreProvider Provider { get; set; }
}
