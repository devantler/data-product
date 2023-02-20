using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Apis.Rest;

/// <summary>
/// Generic interface for REST Controllers.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IController<T>
{
    /// <summary>
    /// Abstract method for reading an entity.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<T>> Read(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Abstract method for creating an entity.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<T>> Create(T model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Abstract method for updating an entity.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<T>> Update(T model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Abstract method for deleting an entity.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<T>> Delete(Guid id, CancellationToken cancellationToken = default);
}
