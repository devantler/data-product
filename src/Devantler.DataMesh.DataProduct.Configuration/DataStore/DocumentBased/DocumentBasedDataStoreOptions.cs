namespace Devantler.DataMesh.DataProduct.Configuration.DataStore.DocumentBased;

/// <summary>
/// Options to configure a document-based data store.
/// </summary>
public class DocumentBasedDataStoreOptions : DataStoreOptionsBase
{
    /// <inheritdoc/>
    public override DataStoreType Type { get; set; } = DataStoreType.DocumentBased;

    /// <summary>
    /// The document-based data store provider to use.
    /// </summary>
    public DocumentBasedDataStoreProvider Provider { get; set; } = DocumentBasedDataStoreProvider.Auto;
}
