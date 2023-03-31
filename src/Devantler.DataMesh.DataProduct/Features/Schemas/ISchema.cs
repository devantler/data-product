namespace Devantler.DataMesh.DataProduct.Features.Schemas;

/// <summary>
/// An interface for schemas.
/// </summary>
public interface ISchema<T>
{
    /// <summary>
    /// The unique identifier of the schema.
    /// </summary>
    public T Id { get; set; }
}
