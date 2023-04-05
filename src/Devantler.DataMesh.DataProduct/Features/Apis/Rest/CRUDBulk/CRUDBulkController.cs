using Devantler.DataMesh.DataProduct.Features.Apis.Rest.CRUD;
using Devantler.DataMesh.DataProduct.Features.DataStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Features.Apis.Rest.CRUDBulk;

/// <summary>
/// A controller to handle REST API requests for a schema.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TSchema"></typeparam>
[ApiController]
[Route("[controller]")]
public abstract class CRUDBulkController<TKey, TSchema> : ControllerBase, ICRUDBulkController<TKey, TSchema> where TSchema : class, Schemas.ISchema<TKey>
{
    readonly IDataStoreService<TKey, TSchema> _dataStoreService;

    /// <summary>
    /// Constructs a new instance of <see cref="CRUDController{TKey, TSchema}"/> and injects the required services.
    /// </summary>
    /// <param name="dataStoreService"></param>
    protected CRUDBulkController(IDataStoreService<TKey, TSchema> dataStoreService)
        => _dataStoreService = dataStoreService;


    /// <inheritdoc />
    [HttpPost]
    public async Task<ActionResult<IEnumerable<TSchema>>> PostAsync([FromBody] IEnumerable<TSchema> models,
        CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.CreateMultipleAsync(models, false, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TSchema>>> GetAll(CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.ReadAllAsync(cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet("{ids}")]
    public async Task<ActionResult<IEnumerable<TSchema>>> GetByIds([FromRoute] List<TKey> ids,
        CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.ReadMultipleAsync(ids, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet("page/{page}/pageSize/{pageSize}")]
    public async Task<ActionResult<IEnumerable<TSchema>>> GetPageAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.ReadMultipleWithPaginationAsync(page, pageSize, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet("offset/{offset}/limit/{limit}")]
    public async Task<ActionResult<IEnumerable<TSchema>>> GetByOffset(int offset = 0, int limit = 20, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.ReadMultipleWithOffsetAsync(limit, offset, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPut]
    public async Task<ActionResult> PutAsync(IEnumerable<TSchema> models,
        CancellationToken cancellationToken = default)
    {
        await _dataStoreService.UpdateMultipleAsync(models, cancellationToken);
        return Ok();
    }

    /// <inheritdoc />
    [HttpDelete]
    public async Task<ActionResult> DeleteAsync([FromQuery] List<TKey> ids,
        CancellationToken cancellationToken = default)
    {
        await _dataStoreService.DeleteMultipleAsync(ids, cancellationToken);
        return Ok();
    }
}
