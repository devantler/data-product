namespace Devantler.DataMesh.Core.Models;
public class Dataspace
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Enabled { get; set; }
    public Dataspace(string name, string description)
    {
        Name = name;
        Description = description;
    }
}