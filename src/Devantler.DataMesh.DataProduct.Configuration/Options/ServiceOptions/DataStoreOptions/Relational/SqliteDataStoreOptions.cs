namespace Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.DataStoreOptions.Relational;

/// <summary>
/// Options to configure a Sqlite data store.
/// </summary>
public class SqliteDataStoreOptions : RelationalDataStoreOptionsBase
{
    /// <inheritdoc/>
    public override string ConnectionString { get; set; } = "Data Source=sqlite.db";

    /// <inheritdoc/>
    public override string Provider { get; set; } = RelationalDataStoreProvider.Sqlite;
}
