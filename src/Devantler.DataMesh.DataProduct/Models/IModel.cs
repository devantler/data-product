namespace Devantler.DataMesh.DataProduct.Models;

/// <summary>
/// An interface for models.
/// </summary>
public interface IModel
{
    /// <summary>
    /// The unique identifier of the model.
    /// </summary>
    public Guid Id { get; set; }
}
