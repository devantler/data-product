namespace Devantler.DataMesh.DataProduct.Configuration;

public class FeaturesOptions
{
    public const string KEY = "DataProduct:Features";

    public List<string> Apis { get; set; } = new();
    public DataStoreType DataStoreType { get; set; }
    public DataStoreProvider DataStoreProvider { get; set; } = DataStoreProvider.Auto;
    public bool Caching { get; set; }
    public bool Metadata { get; set; }
    public bool Authentication { get; set; }
    public bool Authorisation { get; set; }
    public bool Metrics { get; set; }
    public bool Tracing { get; set; }
    public bool Logging { get; set; }
    public bool Health { get; set; }
}

public enum DataStoreType
{
    Relational,
    Document,
    Graph
}

public enum DataStoreProvider
{
    Auto,
    Sqlite,
    MongoDb,
    Neo4J,
}
