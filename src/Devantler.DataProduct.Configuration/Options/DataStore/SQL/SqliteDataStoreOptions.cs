namespace Devantler.DataProduct.Configuration.Options.DataStore.SQL;

/// <summary>
/// Options to configure a SQLite data store.
/// </summary>
public class SQLiteDataStoreOptions : DataStoreOptions
{
  /// <inheritdoc/>
  public override DataStoreType Type { get; set; } = DataStoreType.SQL;

  /// <inheritdoc/>
  public override string ConnectionString { get; set; } = "Data Source=sqlite.db";

  /// <inheritdoc/>
  public override string Provider { get; set; } = SQLDataStoreProvider.SQLite;
}
