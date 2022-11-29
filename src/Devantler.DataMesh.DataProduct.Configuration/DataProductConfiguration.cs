namespace Devantler.DataMesh.DataProduct.Configuration;

public class DataProductConfiguration
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public Owner Owner { get; set; } = new();
    public Features Features { get; set; } = new();
    public List<Schema> Schemas { get; set; } = new();
}
