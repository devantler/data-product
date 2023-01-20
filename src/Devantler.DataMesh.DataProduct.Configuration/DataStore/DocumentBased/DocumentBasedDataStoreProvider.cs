namespace Devantler.DataMesh.DataProduct.Configuration.DataStore.DocumentBased;

/// <summary>
/// Supported data store providers for the document-based data store type.
/// </summary>
public enum DocumentBasedDataStoreProvider
{
    /// <summary>
    /// Automatically decide the which data store provider to use for the document-based data store type.
    /// </summary>
    Auto = 0,

    /// <summary>
    /// MongoDb a document-based data store provider
    /// </summary>
    MongoDb = 1
}
