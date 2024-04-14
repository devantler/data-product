namespace Devantler.DataProduct.Configuration.Options.DataStore.NoSQL;

/// <summary>
/// Options to configure a MongoDb data store.
/// </summary>
public class MongoDbDataStoreOptions : DataStoreOptions
{
  /// <inheritdoc/>
  public override DataStoreType Type { get; set; } = DataStoreType.NoSQL;

  /// <inheritdoc/>
  public override string Provider { get; set; } = NoSQLDataStoreProvider.LiteDb;
}
