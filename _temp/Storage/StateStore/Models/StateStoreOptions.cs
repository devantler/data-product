namespace Devantler.DataMesh.DataProduct.Storage.StateStore.DTOs;

public class StateStoreOptions
{
    public required string Name { get; set; }
    public int? Parallelism { get; set; }
}
