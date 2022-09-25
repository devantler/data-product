namespace DevAntler.DataMesh.Core.Models;
public class DataProduct
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Enabled { get; set; }
    public DataProduct(string name, string description)
    {
        Name = name;
        Description = description;
    }
}