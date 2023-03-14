using Devantler.DataMesh.DataProduct.Features.DataStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Features.Apis.Rest;

/// <summary>
/// A controller to handle REST API requests for a schema.
/// </summary>
/// <typeparam name="TSchema"></typeparam>
[ApiController]
[Route("[controller]")]
public abstract class RestApiController<TSchema> : ControllerBase, IController<TSchema> where TSchema : class, Schemas.ISchema
{
    readonly IDataStoreService<TSchema> _dataStoreService;

    /// <summary>
    /// Constructs a new instance of <see cref="RestApiController{TSchema}"/> and injects the required services.
    /// </summary>
    /// <param name="dataStoreService"></param>
    protected RestApiController(IDataStoreService<TSchema> dataStoreService)
        => _dataStoreService = dataStoreService;

    /// <inheritdoc />
    [HttpGet("{id}")]
    public async Task<ActionResult<TSchema>> GetSingleAsync(string id, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.GetSingleAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TSchema>>> GetMultipleAsync([FromQuery] List<string> ids,
        CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.GetMultipleAsync(ids, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet("paged")]
    public async Task<ActionResult<IEnumerable<TSchema>>> GetMultipleWithPaginationAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.GetMultipleWithPaginationAsync(page, pageSize, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet("limited")]
    public async Task<ActionResult<IEnumerable<TSchema>>> GetMultipleWithLimitAsync([FromQuery] int limit = 20, [FromQuery] int offset = 0, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.GetMultipleWithLimitAsync(limit, offset, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPost("single")]
    public async Task<ActionResult<TSchema>> PostSingleAsync([FromBody] TSchema schema, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.CreateSingleAsync(schema, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPost]
    public async Task<ActionResult<int>> PostMultipleAsync([FromBody] IEnumerable<TSchema> models,
        CancellationToken cancellationToken = default)
    {
        int result = await _dataStoreService.CreateMultipleAsync(models, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPut("single")]
    public async Task<ActionResult<TSchema>> PutSingleAsync(TSchema schema, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.UpdateSingleAsync(schema, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPut]
    public async Task<ActionResult<int>> PutMultipleAsync(IEnumerable<TSchema> models,
        CancellationToken cancellationToken = default)
    {
        int result = await _dataStoreService.UpdateMultipleAsync(models, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpDelete("{id}")]
    public async Task<ActionResult<TSchema>> DeleteSingleAsync(string id, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.DeleteSingleAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpDelete]
    public async Task<ActionResult<IEnumerable<TSchema>>> DeleteMultipleAsync([FromQuery] List<string> ids,
        CancellationToken cancellationToken = default)
    {
        int result = await _dataStoreService.DeleteMultipleAsync(ids, cancellationToken);
        return Ok(result);
    }
}
