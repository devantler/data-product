namespace Devantler.DataMesh.DataProduct.Configuration.Options.DataStoreOptions.DocumentBased;

/// <summary>
/// Options to configure a document-based data store.
/// </summary>
public abstract class DocumentBasedDataStoreOptionsBase : DataStoreOptionsBase
{
    /// <inheritdoc/>
    public override DataStoreType Type { get; set; } = DataStoreType.DocumentBased;

    /// <summary>
    /// The document-based data store provider to use.
    /// </summary>
    public abstract DocumentBasedDataStoreProvider Provider { get; set; }
}
