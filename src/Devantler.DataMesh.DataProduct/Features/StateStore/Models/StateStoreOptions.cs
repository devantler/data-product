namespace Devantler.DataMesh.DataProduct.Features.StateStore.Models;

public class StateStoreOptions
{
    public required string Name { get; set; }
    public int? Parallelism { get; set; }
}
