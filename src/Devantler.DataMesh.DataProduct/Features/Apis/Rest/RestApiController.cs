using Devantler.DataMesh.DataProduct.Features.DataStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Features.Apis.Rest;

/// <summary>
/// A controller to handle REST API requests for a schema.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TSchema"></typeparam>
[ApiController]
[Route("[controller]")]
public abstract class RestApiController<TKey, TSchema> : ControllerBase, IController<TKey, TSchema> where TSchema : class, Schemas.ISchema<TKey>
{
    readonly IDataStoreService<TKey, TSchema> _dataStoreService;

    /// <summary>
    /// Constructs a new instance of <see cref="RestApiController{TKey, TSchema}"/> and injects the required services.
    /// </summary>
    /// <param name="dataStoreService"></param>
    protected RestApiController(IDataStoreService<TKey, TSchema> dataStoreService)
        => _dataStoreService = dataStoreService;

    /// <inheritdoc />
    [HttpGet("{id}")]
    public async Task<ActionResult<TSchema>> GetById(TKey id, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.GetSingleAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet("bulk")]
    public async Task<ActionResult<IEnumerable<TSchema>>> GetByIds([FromQuery] List<TKey> ids,
        CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.GetMultipleAsync(ids, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet("page")]
    public async Task<ActionResult<IEnumerable<TSchema>>> GetPageAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.GetMultipleWithPaginationAsync(page, pageSize, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet("offset")]
    public async Task<ActionResult<IEnumerable<TSchema>>> GetByOffset([FromQuery] int limit = 20, [FromQuery] int offset = 0, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.GetMultipleWithLimitAsync(limit, offset, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPost]
    public async Task<ActionResult<TSchema>> PostAsync([FromBody] TSchema schema, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.CreateSingleAsync(schema, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPost("bulk")]
    public async Task<ActionResult<int>> PostAsync([FromBody] IEnumerable<TSchema> models,
        CancellationToken cancellationToken = default)
    {
        int result = await _dataStoreService.CreateMultipleAsync(models, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPut]
    public async Task<ActionResult<TSchema>> PutAsync(TSchema schema, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.UpdateSingleAsync(schema, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPut("bulk")]
    public async Task<ActionResult<int>> PutAsync(IEnumerable<TSchema> models,
        CancellationToken cancellationToken = default)
    {
        int result = await _dataStoreService.UpdateMultipleAsync(models, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpDelete("{id}")]
    public async Task<ActionResult<TSchema>> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.DeleteSingleAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpDelete("bulk")]
    public async Task<ActionResult<IEnumerable<TSchema>>> DeleteAsync([FromQuery] List<TKey> ids,
        CancellationToken cancellationToken = default)
    {
        int result = await _dataStoreService.DeleteMultipleAsync(ids, cancellationToken);
        return Ok(result);
    }
}
