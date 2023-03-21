namespace Devantler.DataMesh.DataProduct.Configuration.Options.DataStore.SQL;

/// <summary>
/// Options to configure a Sqlite data store.
/// </summary>
public class SqliteDataStoreOptions : IDataStoreOptions
{
    /// <inheritdoc/>
    public DataStoreType Type { get; set; } = DataStoreType.SQL;

    /// <inheritdoc/>
    public string ConnectionString { get; set; } = "Data Source=sqlite.db";

    /// <inheritdoc/>
    public string Provider { get; set; } = SQLDataStoreProvider.Sqlite;
}
