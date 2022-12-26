namespace Devantler.DataMesh.DataProduct.Configuration;

public class SchemaRegistryOptions
{
    public const string KEY = "DataProduct:SchemaRegistry";

    public SchemaRegistryType Type { get; set; }

    public string? Path { get; set; }

    public string? Url { get; set; }
}

public enum SchemaRegistryType
{
    Local,
    Kafka
}
