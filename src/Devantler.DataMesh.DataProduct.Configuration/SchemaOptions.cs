namespace Devantler.DataMesh.DataProduct.Configuration;

public class SchemaOptions
{
    public const string KEY = "DataProduct:Schema";

    public string Subject { get; set; } = string.Empty;

    public int Version { get; set; }
}
