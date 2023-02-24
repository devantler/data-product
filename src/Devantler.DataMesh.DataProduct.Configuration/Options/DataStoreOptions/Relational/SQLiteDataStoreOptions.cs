namespace Devantler.DataMesh.DataProduct.Configuration.Options.DataStoreOptions.Relational;

/// <summary>
/// Options to configure a Sqlite data store.
/// </summary>
public class SqliteDataStoreOptions : RelationalDataStoreOptionsBase
{
    /// <inheritdoc/>
    public override RelationalDataStoreProvider Provider { get; set; } = RelationalDataStoreProvider.Sqlite;
    /// <inheritdoc/>
    public override string ConnectionString { get; set; } = "Data Source=sqlite.db";
}
