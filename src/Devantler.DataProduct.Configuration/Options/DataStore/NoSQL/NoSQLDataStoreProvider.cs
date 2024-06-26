namespace Devantler.DataProduct.Configuration.Options.DataStore.NoSQL;

/// <summary>
/// The NoSQL data store provider to use.
/// </summary>
public abstract class NoSQLDataStoreProvider : IProvider
{
  /// <summary>
  /// The MongoDb data store provider. Not supported yet.
  /// </summary>
  public const string MongoDb = "MongoDb";

  /// <summary>
  /// The LiteDb data store provider. Not supported yet.
  /// </summary>
  public const string LiteDb = "LiteDb";

  /// <summary>
  /// The UnQLite data store provider. Not supported yet.
  /// </summary>
  public const string UnQLite = "UnQLite";

  /// <summary>
  /// The RavenDb data store provider. Not supported yet.
  /// </summary>
  public const string RavenDb = "RavenDb";

  /// <summary>
  /// The CouchDb data store provider. Not supported yet.
  /// </summary>
  public const string CouchDb = "CouchDb";
}
