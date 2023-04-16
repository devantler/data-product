using Devantler.DataProduct.Core.Schemas;
using Devantler.DataProduct.Features.DataStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataProduct.Features.Apis.Rest.Controllers;

/// <summary>
/// A controller to handle REST API requests for a schema.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TSchema"></typeparam>
[ApiController]
[Route("[controller]")]
public abstract class RestBulkController<TKey, TSchema> : ControllerBase where TSchema : class, ISchema<TKey>
{
    readonly IDataStoreService<TKey, TSchema> _dataStoreService;

    /// <summary>
    /// Constructs a new instance of <see cref="RestBulkController{TKey, TSchema}"/> and injects the required services.
    /// </summary>
    /// <param name="dataStoreService"></param>
    protected RestBulkController(IDataStoreService<TKey, TSchema> dataStoreService)
        => _dataStoreService = dataStoreService;

    /// <summary>
    /// Creates multiple entities.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost]
    public async Task<ActionResult<IEnumerable<TSchema>>> PostAsync([FromBody] IEnumerable<TSchema> models,
        CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.CreateMultipleAsync(models, false, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Reads all entities.
    /// </summary>
    /// <param name="cancellationToken"></param>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TSchema>>> GetAsync(CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.ReadAllAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Reads paged entities.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    [HttpGet("page/{page}/pageSize/{pageSize}")]
    public async Task<ActionResult<IEnumerable<TSchema>>> GetPageAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.ReadMultipleWithPaginationAsync(page, pageSize, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Reads entities by offset.
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="limit"></param>
    /// <param name="cancellationToken"></param>
    [HttpGet("offset/{offset}/limit/{limit}")]
    public async Task<ActionResult<IEnumerable<TSchema>>> GetByOffset(int offset = 0, int limit = 20, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.ReadMultipleWithOffsetAsync(limit, offset, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Updates multiple entities.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    [HttpPut]
    public async Task<ActionResult> PutAsync(IEnumerable<TSchema> models,
        CancellationToken cancellationToken = default)
    {
        await _dataStoreService.UpdateMultipleAsync(models, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Deletes multiple entities.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    [HttpDelete]
    public async Task<ActionResult> DeleteAsync([FromQuery] List<TKey> ids,
        CancellationToken cancellationToken = default)
    {
        await _dataStoreService.DeleteMultipleAsync(ids, cancellationToken);
        return Ok();
    }
}
