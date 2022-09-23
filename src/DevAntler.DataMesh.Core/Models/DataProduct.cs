namespace DevAntler.DataMesh.Core.Models;
public class DataProduct
{
    public DataProduct(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Enabled { get; set; } = false;
}