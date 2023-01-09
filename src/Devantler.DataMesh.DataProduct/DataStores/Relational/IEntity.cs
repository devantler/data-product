namespace Devantler.DataMesh.DataProduct.DataStores.Relational;

/// <summary>
/// Interface for entities.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// A unique identifier for the entity.
    /// </summary>
    public Guid Id { get; set; }
}
