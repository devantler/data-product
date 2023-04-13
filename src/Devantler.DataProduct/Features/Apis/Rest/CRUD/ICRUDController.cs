using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataProduct.Features.Apis.Rest.CRUD;

/// <summary>
/// Generic interface for CRUD Controllers.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TSchema"></typeparam>
public interface ICRUDController<TKey, TSchema> where TSchema : class, Schemas.ISchema<TKey>
{
    /// <summary>
    /// Creates an entity.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<TSchema>> PostAsync(TSchema model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Read an entity by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<TSchema>> GetById(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an entity.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult> PutAsync(TKey id, TSchema model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
}
