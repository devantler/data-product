namespace Devantler.DataMesh.DataProduct.Configuration;

public class Features
{
    public List<string> Apis { get; set; } = new();
    public DataStore DataStore { get; set; } = DataStore.Auto;
    public bool Caching { get; set; } = false;
    public bool Metadata { get; set; } = false;
    public bool Authentication { get; set; } = false;
    public bool Authorisation { get; set; } = false;
    public bool Metrics { get; set; } = false;
    public bool Tracing { get; set; } = false;
    public bool Logging { get; set; } = false;
    public bool Health { get; set; } = false;
}
public enum DataStore
{
    SQLite,
    PostgreSQL,
    MongoDB,
    Auto
}
