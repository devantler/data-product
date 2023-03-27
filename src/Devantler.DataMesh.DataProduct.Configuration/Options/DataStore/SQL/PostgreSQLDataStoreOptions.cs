namespace Devantler.DataMesh.DataProduct.Configuration.Options.DataStore.SQL;

/// <summary>
/// Options to configure a PostgreSQL data store.
/// </summary>
public class PostgreSQLDataStoreOptions : DataStoreOptions
{
    /// <inheritdoc/>
    public override DataStoreType Type { get; set; } = DataStoreType.SQL;

    /// <inheritdoc/>
    public override string Provider { get; set; } = SQLDataStoreProvider.PostgreSQL;
}
