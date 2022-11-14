namespace Devantler.DataMesh.ProvisioningApi.Models;
public class DataProduct
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Description { get; set; }
    public DataProduct(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
