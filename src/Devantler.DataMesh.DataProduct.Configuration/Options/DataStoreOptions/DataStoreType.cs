namespace Devantler.DataMesh.DataProduct.Configuration.Options.DataStoreOptions;

/// <summary>
/// Supported data store types.
/// </summary>
public enum DataStoreType
{
    /// <summary>
    /// A relational data store.
    /// </summary>
    Relational = 0,

    /// <summary>
    /// A document-based data store.
    /// </summary>
    DocumentBased = 1,
    /// <summary>
    /// A graph-based data store.
    /// </summary>
    GraphBased = 2
}
