namespace Devantler.DataMesh.DataProduct.Configuration.DataStore.GraphBased;

/// <summary>
/// Supported data store providers for the graph-based data store type.
/// </summary>
public enum GraphBasedDataStoreProvider
{
    /// <summary>
    /// Automatically decide the which data store provider to use for the graph-based data store type.
    /// </summary>
    Auto,

    /// <summary>
    /// Neo4j a graph-based data store provider.
    /// </summary>
    Neo4j
}
