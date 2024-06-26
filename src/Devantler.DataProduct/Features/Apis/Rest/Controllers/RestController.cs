using Devantler.DataProduct.Features.DataStore.Services;
using Devantler.DataProduct.Features.Schemas;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataProduct.Features.Apis.Rest.Controllers;

/// <summary>
/// A controller to handle REST API requests for a schema.
/// </summary>
/// <remarks>
/// The controller uses the OData protocol.
/// </remarks>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TSchema"></typeparam>
/// <remarks>
/// Constructs a new instance of <see cref="RestController{TKey, TSchema}"/> and injects the required services.
/// </remarks>
/// <param name="dataStoreService"></param>
[ApiController]
[Route("[controller]")]
public abstract class RestController<TKey, TSchema>(IDataStoreService<TKey, TSchema> dataStoreService) : ControllerBase where TSchema : class, ISchema<TKey>
{
  readonly IDataStoreService<TKey, TSchema> _dataStoreService = dataStoreService;

  /// <summary>
  /// Creates a new entity.
  /// </summary>
  /// <param name="model"></param>
  /// <param name="cancellationToken"></param>
  [HttpPost]
  public async Task<ActionResult<TSchema?>> PostAsync(TSchema model, CancellationToken cancellationToken = default)
      => await _dataStoreService.CreateSingleAsync(model, cancellationToken);

  /// <summary>
  /// Reads a single entity.
  /// </summary>
  /// <param name="id"></param>
  /// <param name="cancellationToken"></param>
  [HttpGet("{id}")]
  public async Task<ActionResult<TSchema>> GetAsync(TKey id, CancellationToken cancellationToken = default)
  {
    var result = await _dataStoreService.ReadSingleAsync(id, cancellationToken);
    return Ok(result);
  }

  /// <summary>
  /// Updates a single entity.
  /// </summary>
  /// <param name="model"></param>
  /// <param name="cancellationToken"></param>
  [HttpPut("{id}")]
  public async Task<ActionResult> PutAsync(TSchema model, CancellationToken cancellationToken = default)
  {
    await _dataStoreService.UpdateSingleAsync(model, cancellationToken);
    return Ok();
  }

  /// <summary>
  /// Deletes a single entity.
  /// </summary>
  /// <param name="id"></param>
  /// <param name="cancellationToken"></param>
  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
  {
    await _dataStoreService.DeleteSingleAsync(id, cancellationToken);
    return Ok();
  }
}
