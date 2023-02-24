using Devantler.DataMesh.DataProduct.DataStore.Entities;
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
public class RestApiController<TModel, TEntity> : ControllerBase, IController<TModel>
    where TModel : class
    where TEntity : class, IEntity
{
    readonly DataStoreService<TModel, TEntity> _dataStoreService;

    /// <summary>
    /// Constructs a new instance of <see cref="RestApiController{TModel, TEntity}"/> and injects the required services.
    /// </summary>
    /// <param name="dataStoreService"></param>
    public RestApiController(DataStoreService<TModel, TEntity> dataStoreService)
        => _dataStoreService = dataStoreService;

    /// <inheritdoc />
    [HttpGet("{id}")]
    public async Task<ActionResult<TModel>> ReadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.ReadAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TModel>>> ReadManyAsync([FromQuery] List<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.ReadManyAsync(ids, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet("paged")]
    public async Task<ActionResult<IEnumerable<TModel>>> ReadPagedAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.ReadPagedAsync(page, pageSize, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<TModel>>> ReadListAsync([FromQuery] int limit = 20, [FromQuery] int offset = 0, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.ReadListAsync(limit, offset, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPost]
    public async Task<ActionResult<TModel>> CreateAsync([FromBody] TModel model, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.CreateAsync(model, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPost("many")]
    public async Task<ActionResult<int>> CreateManyAsync([FromBody] IEnumerable<TModel> models,
        CancellationToken cancellationToken = default)
    {
        int result = await _dataStoreService.CreateManyAsync(models, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPut("{id}")]
    public async Task<ActionResult<TModel>> UpdateAsync(TModel model, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.UpdateAsync(model, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPut]
    public async Task<ActionResult<int>> UpdateManyAsync(IEnumerable<TModel> models,
        CancellationToken cancellationToken = default)
    {
        int result = await _dataStoreService.UpdateManyAsync(models, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpDelete("{id}")]
    public async Task<ActionResult<TModel>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _dataStoreService.DeleteAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpDelete]
    public async Task<ActionResult<IEnumerable<TModel>>> DeleteManyAsync([FromQuery] List<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        int result = await _dataStoreService.DeleteManyAsync(ids, cancellationToken);
        return Ok(result);
    }
}
