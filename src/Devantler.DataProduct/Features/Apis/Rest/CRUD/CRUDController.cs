using Devantler.DataProduct.Features.DataStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataProduct.Features.Apis.Rest.CRUD;

/// <summary>
/// A controller to handle REST API requests for a schema.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TSchema"></typeparam>
[ApiController]
[Route("[controller]")]
public abstract class CRUDController<TKey, TSchema> : ControllerBase, ICRUDController<TKey, TSchema> where TSchema : class, Schemas.ISchema<TKey>
{
    readonly IDataStoreService<TKey, TSchema> _dataStoreService;

    /// <summary>
    /// Constructs a new instance of <see cref="CRUDController{TKey, TSchema}"/> and injects the required services.
    /// </summary>
    /// <param name="dataStoreService"></param>
    protected CRUDController(IDataStoreService<TKey, TSchema> dataStoreService)
        => _dataStoreService = dataStoreService;

    /// <inheritdoc />
    [HttpPost]
    public async Task<ActionResult<TSchema>> PostAsync(TSchema model, CancellationToken cancellationToken = default)
        => await _dataStoreService.CreateSingleAsync(model, cancellationToken);

    /// <inheritdoc />
    [HttpGet("{id}")]
    public async Task<ActionResult<TSchema>> GetById(TKey id, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.ReadSingleAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPut("{id}")]
    public async Task<ActionResult> PutAsync(TKey id, TSchema model, CancellationToken cancellationToken = default)
    {
        model.Id = id;
        await _dataStoreService.UpdateSingleAsync(model, cancellationToken);
        return Ok();
    }

    /// <inheritdoc />
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
    {
        await _dataStoreService.DeleteSingleAsync(id, cancellationToken);
        return Ok();
    }
}
