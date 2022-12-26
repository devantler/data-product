namespace Devantler.DataMesh.DataProduct.Configuration;

public class DataProductOptions
{
    public const string KEY = "DataProduct";

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public OwnerOptions Owner { get; set; } = new();
    public FeaturesOptions Features { get; set; } = new();
    public SchemaOptions Schema { get; set; } = new();
    public SchemaRegistryOptions SchemaRegistry { get; set; } = new();
}
