namespace Devantler.DataMesh.DataProduct.Configuration.DataStore.GraphBased;

/// <summary>
/// Options to configure a graph-based data store.
/// </summary>
public class GraphBasedDataStoreOptions : DataStoreOptionsBase
{
    /// <inheritdoc/>
    public override DataStoreType Type { get; set; } = DataStoreType.GraphBased;

    /// <summary>
    /// The graph-based data store provider to use.
    /// </summary>
    public GraphBasedDataStoreProvider Provider { get; set; } = GraphBasedDataStoreProvider.Auto;
}
