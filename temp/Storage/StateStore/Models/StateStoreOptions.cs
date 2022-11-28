namespace Devantler.DataMesh.DataProduct.Storage.StateStore.Models;

public class StateStoreOptions
{
    public required string Name { get; set; }
    public int? Parallelism { get; set; }
}
