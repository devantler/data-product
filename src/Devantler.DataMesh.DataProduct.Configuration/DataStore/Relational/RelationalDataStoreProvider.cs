namespace Devantler.DataMesh.DataProduct.Configuration.DataStore.Relational;

/// <summary>
/// Supported data store providers for the relational data store type.
/// </summary>
public enum RelationalDataStoreProvider
{
    /// <summary>
    /// Automatically decide the which data store provider to use for the relational data store type.
    /// </summary>
    Auto,

    /// <summary>
    /// SQLite a relational data store provider.
    /// </summary>
    SQlite
}
