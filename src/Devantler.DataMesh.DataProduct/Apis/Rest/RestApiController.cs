using Devantler.DataMesh.DataProduct.DataStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Apis.Rest;

/// <summary>
/// A controller to handle REST API requests for a model.
/// </summary>
/// <typeparam name="TModel"></typeparam>
/// <typeparam name="TEntity"></typeparam>
[ApiController]
[Route("[controller]")]
public class RestApiController<TModel> : ControllerBase, IController<TModel>
    where TModel : class
{
    readonly IDataStoreService<TModel> _dataStoreService;

    /// <summary>
    /// Constructs a new instance of <see cref="RestApiController{TModel}"/> and injects the required services.
    /// </summary>
    /// <param name="dataStoreService"></param>
    public RestApiController(IDataStoreService<TModel> dataStoreService)
        => _dataStoreService = dataStoreService;

    /// <inheritdoc />
    [HttpGet("{id}")]
    public async Task<ActionResult<TModel>> GetSingleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.GetSingleAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet]
    public async Task<ActionResult<IQueryable<TModel>>> GetMultipleAsync([FromQuery] List<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.GetMultipleAsync(ids, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet("paged")]
    public async Task<ActionResult<IQueryable<TModel>>> GetMultipleWithPaginationAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.GetMultipleWithPaginationAsync(page, pageSize, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet("limited")]
    public async Task<ActionResult<IQueryable<TModel>>> GetMultipleWithLimitAsync([FromQuery] int limit = 20, [FromQuery] int offset = 0, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.GetMultipleWithLimitAsync(limit, offset, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPost("single")]
    public async Task<ActionResult<TModel>> PostSingleAsync([FromBody] TModel model, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.CreateSingleAsync(model, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPost]
    public async Task<ActionResult<int>> PostMultipleAsync([FromBody] IQueryable<TModel> models,
        CancellationToken cancellationToken = default)
    {
        int result = await _dataStoreService.CreateMultipleAsync(models, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPut("{id}")]
    public async Task<ActionResult<TModel>> PutSingleAsync(TModel model, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.UpdateSingleAsync(model, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPut]
    public async Task<ActionResult<int>> PutMultipleAsync(IQueryable<TModel> models,
        CancellationToken cancellationToken = default)
    {
        int result = await _dataStoreService.UpdateMultipleAsync(models, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpDelete("{id}")]
    public async Task<ActionResult<TModel>> DeleteSingleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.DeleteSingleAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpDelete]
    public async Task<ActionResult<IQueryable<TModel>>> DeleteMultipleAsync([FromQuery] List<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        int result = await _dataStoreService.DeleteMultipleAsync(ids, cancellationToken);
        return Ok(result);
    }
}
