//HintName: RecordSchema.cs
namespace Devantler.DataMesh.DataProduct.Models;

public class RecordSchema : IModel
{
    public Guid Id { get; set; }
        public string Name { get; set; }
    public int Age { get; set; }    
}
