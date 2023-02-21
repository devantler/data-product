using AutoMapper;
using Devantler.DataMesh.DataProduct.DataStore.Interfaces;
using Devantler.DataMesh.DataProduct.DataStore.Relational;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Apis.Rest;

/// <summary>
/// A controller to handle REST API requests for a model.
/// </summary>
/// <typeparam name="TModel"></typeparam>
/// <typeparam name="TEntity"></typeparam>
[ApiController]
[Route("[controller]")]
public class RestApiController<TModel, TEntity> : ControllerBase, IController<TModel> where TEntity : class, IEntity
{
    readonly EntityFrameworkRepository<TEntity> _repository;
    readonly IMapper _mapper;

    /// <summary>
    /// Creates a new instance of the <see cref="StudentController"/> class.
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="mapper"></param>
    public RestApiController(EntityFrameworkRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    [HttpGet("{id}")]
    public async Task<ActionResult<TModel>> ReadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.ReadAsync(id, cancellationToken);
        var result = _mapper.Map<TEntity, TModel>(entity);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TModel>>> ReadManyAsync([FromQuery] List<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        var entities = await _repository.ReadManyAsync(ids, cancellationToken);
        var result = _mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(entities);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet("paged")]
    public async Task<ActionResult<IEnumerable<TModel>>> ReadPagedAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var entities = await _repository.ReadPagedAsync(page, pageSize, cancellationToken);
        var result = _mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(entities);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<TModel>>> ReadListAsync([FromQuery] int limit = 20, [FromQuery] int offset = 0, CancellationToken cancellationToken = default)
    {
        var entities = await _repository.ReadListAsync(limit, offset, cancellationToken);
        var result = _mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(entities);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPost]
    public async Task<ActionResult<TModel>> CreateAsync([FromBody] TModel model, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TModel, TEntity>(model);
        var createdEntity = await _repository.CreateAsync(entity, cancellationToken);
        var result = _mapper.Map<TEntity, TModel>(createdEntity);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPost("many")]
    public async Task<ActionResult<int>> CreateManyAsync([FromBody] IEnumerable<TModel> models,
        CancellationToken cancellationToken = default)
    {
        var entities = _mapper.Map<IEnumerable<TModel>, IEnumerable<TEntity>>(models);
        int result = await _repository.CreateManyAsync(entities, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPut("{id}")]
    public async Task<ActionResult<TModel>> UpdateAsync(TModel model, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TModel, TEntity>(model);
        var updatedEntity = await _repository.UpdateAsync(entity, cancellationToken);
        var result = _mapper.Map<TEntity, TModel>(updatedEntity);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPut]
    public async Task<ActionResult<int>> UpdateManyAsync(IEnumerable<TModel> models,
        CancellationToken cancellationToken = default)
    {
        var entities = _mapper.Map<IEnumerable<TModel>, IEnumerable<TEntity>>(models);
        int result = await _repository.UpdateManyAsync(entities, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpDelete("{id}")]
    public async Task<ActionResult<TModel>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var deletedEntity = await _repository.DeleteAsync(id, cancellationToken);
        var result = _mapper.Map<TEntity, TModel>(deletedEntity);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpDelete]
    public async Task<ActionResult<IEnumerable<TModel>>> DeleteManyAsync([FromQuery] List<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        int result = await _repository.DeleteManyAsync(ids, cancellationToken);
        return Ok(result);
    }
}
