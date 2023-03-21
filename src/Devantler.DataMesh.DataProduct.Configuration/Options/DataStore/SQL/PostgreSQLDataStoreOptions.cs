namespace Devantler.DataMesh.DataProduct.Configuration.Options.DataStore.SQL;

/// <summary>
/// Options to configure a PostgreSQL data store.
/// </summary>
public class PostgreSQLDataStoreOptions : IDataStoreOptions
{
    /// <inheritdoc/>
    public DataStoreType Type { get; set; } = DataStoreType.SQL;

    /// <inheritdoc/>
    public string Provider { get; set; } = SQLDataStoreProvider.PostgreSQL;

    /// <inheritdoc/>
    public string ConnectionString { get; set; } = string.Empty;
}
