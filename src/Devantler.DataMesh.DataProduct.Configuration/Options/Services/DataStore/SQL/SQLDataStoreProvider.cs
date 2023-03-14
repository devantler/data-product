namespace Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataStore.SQL;

/// <summary>
/// Supported data store providers for the relational data store type.
/// </summary>
public class SQLDataStoreProvider : IProvider
{
    /// <summary>
    /// The Sqlite data store provider.
    /// </summary>
    public const string Sqlite = "Sqlite";

    /// <summary>
    /// The MariaDb data store provider. Not supported yet.
    /// </summary>
    public const string MariaDb = "MariaDb";

    /// <summary>
    /// The MySql data store provider. Not supported yet.
    /// </summary>
    public const string MySql = "MySql";

    /// <summary>
    /// The PostgreSql data store provider.
    /// </summary>
    public const string PostgreSQL = "PostgreSQL";
}
