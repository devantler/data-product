namespace Devantler.DataMesh.DataProduct.Configuration;

public abstract class Schema
{
    public string Name { get; set; } = string.Empty;

    public string Version { get; set; } = string.Empty;

    public List<Property> Properties { get; set; } = new();

    public abstract class Property
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
