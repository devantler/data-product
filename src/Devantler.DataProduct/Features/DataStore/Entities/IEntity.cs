namespace Devantler.DataProduct.Features.DataStore.Entities;

/// <summary>
/// An interface for entities.
/// </summary>
public interface IEntity<T>
{
  /// <summary>
  /// A unique identifier for the entity.
  /// </summary>
  public T Id { get; set; }
}
