using AutoMapper;
using Devantler.DataMesh.DataProduct.DataStore.Relational.Entities;
using Devantler.DataMesh.DataProduct.DataStore.Relational.Repositories;
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

    /// <summary>
    /// Reads an entity by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    [HttpGet("{id}")]
    public async Task<ActionResult<TModel>> ReadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.ReadAsync(id, cancellationToken);
        var result = _mapper.Map<TEntity, TModel>(entity);
        return Ok(result);
    }

    /// <summary>
    /// Reads one or more entities by id.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TModel>>> ReadManyAsync([FromQuery] List<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        var entities = await _repository.ReadManyAsync(ids, cancellationToken);
        var result = _mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(entities);
        return Ok(result);
    }

    /// <summary>
    /// Reads entities through pagination.
    /// </summary>
    /// <param name="pages"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    [HttpGet("paged")]
    public async Task<ActionResult<IEnumerable<TModel>>> ReadPagedAsync([FromQuery] int pages = 1, [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var entities = await _repository.ReadPagedAsync(pages, pageSize, cancellationToken);
        var result = _mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(entities);
        return Ok(result);
    }

    /// <summary>
    /// Creates an entity.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost]
    public async Task<ActionResult<TModel>> CreateAsync([FromBody] TModel model, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TModel, TEntity>(model);
        var createdEntity = await _repository.CreateAsync(entity, cancellationToken);
        var result = _mapper.Map<TEntity, TModel>(createdEntity);
        return Ok(result);
    }

    /// <summary>
    /// Creates one or more entities.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost("many")]
    public async Task<ActionResult<int>> CreateManyAsync([FromBody] IEnumerable<TModel> models,
        CancellationToken cancellationToken = default)
    {
        var entities = _mapper.Map<IEnumerable<TModel>, IEnumerable<TEntity>>(models);
        int result = await _repository.CreateManyAsync(entities, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Updates an entity.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    [HttpPut("{id}")]
    public async Task<ActionResult<TModel>> UpdateAsync(TModel model, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TModel, TEntity>(model);
        var updatedEntity = await _repository.UpdateAsync(entity, cancellationToken);
        var result = _mapper.Map<TEntity, TModel>(updatedEntity);
        return Ok(result);
    }

    /// <summary>
    /// Updates one or more entities.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    [HttpPut]
    public async Task<ActionResult<int>> UpdateManyAsync(IEnumerable<TModel> models,
        CancellationToken cancellationToken = default)
    {
        var entities = _mapper.Map<IEnumerable<TModel>, IEnumerable<TEntity>>(models);
        int result = await _repository.UpdateManyAsync(entities, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    [HttpDelete("{id}")]
    public async Task<ActionResult<TModel>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var deletedEntity = await _repository.DeleteAsync(id, cancellationToken);
        var result = _mapper.Map<TEntity, TModel>(deletedEntity);
        return Ok(result);
    }

    /// <summary>
    /// Deletes one or more entities.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    [HttpDelete]
    public async Task<ActionResult<IEnumerable<TModel>>> DeleteManyAsync([FromQuery] List<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        int result = await _repository.DeleteManyAsync(ids, cancellationToken);
        return Ok(result);
    }
}
