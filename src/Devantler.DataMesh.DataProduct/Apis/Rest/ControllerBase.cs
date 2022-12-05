using AutoMapper;
using Devantler.DataMesh.DataProduct.DataStores.Relational.Entities;
using Devantler.DataMesh.DataProduct.DataStores.Relational.Repositories;
using Devantler.DataMesh.DataProduct.Models;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Apis.Rest;

[ApiController]
[Route("[controller]")]
public abstract class ControllerBase<TModel, TEntity> : ControllerBase, IController<TModel>
    where TModel : IModel
    where TEntity : IEntity
{
    protected readonly IMapper _mapper;
    protected readonly IRepository<TEntity> _repository;

    protected ControllerBase(IRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TModel>>> Get([FromQuery] IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        if (!ids.Any())
            throw new ArgumentException("No ids provided");

        if (ids.Count() == 1)
        {
            var entity = await _repository.Read(ids.First(), cancellationToken);
            var model = _mapper.Map<TModel>(entity);
            return Ok(model);
        }
        else
        {
            var entities = await _repository.ReadMany(ids, cancellationToken);
            var models = _mapper.Map<IEnumerable<TModel>>(entities);
            return Ok(models);
        }
    }

    [HttpGet("paged")]
    public async Task<ActionResult<IEnumerable<TModel>>> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var entities = await _repository.ReadPaged(page, pageSize, cancellationToken);
        var models = _mapper.Map<IEnumerable<TModel>>(entities);
        return Ok(models);
    }

    // 
    // public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    // {
    //     await _repository.Delete(id, cancellationToken);
    //     return Ok();
    // }

    // [HttpDelete]
    // public async Task<ActionResult> DeleteMany([FromBody] IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    // {
    //     await _repository.DeleteMany(ids, cancellationToken);
    //     return Ok();
    // }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<TModel>> Get(Guid id, CancellationToken cancellationToken = default)
    // {
    //     var entity = await _repository.Read(id, cancellationToken);
    //     if (entity == null)
    //         return NotFound();

    //     var model = _mapper.Map<TModel>(entity);
    //     return Ok(model);
    // }

    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<TModel>>> GetMany([FromBody] IEnumerable<Guid> ids, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    // {
    //     var entities = await _repository.ReadMany(ids, page, pageSize, cancellationToken);
    //     if (!entities.Any())
    //         return NotFound();

    //     var models = _mapper.Map<IEnumerable<TModel>>(entities);
    //     return Ok(models);
    // }

    // [HttpPost]
    // public async Task<ActionResult<Guid>> Post([FromBody] TModel model, CancellationToken cancellationToken = default)
    // {
    //     var entity = _mapper.Map<TEntity>(model);
    //     var id = await _repository.Create(entity, cancellationToken);
    //     if (id == Guid.Empty)
    //         return BadRequest();

    //     return Ok(id);
    // }

    // [HttpPost("many")]
    // public async Task<ActionResult> PostMany([FromBody] IEnumerable<TModel> models, CancellationToken cancellationToken = default)
    // {
    //     var entities = _mapper.Map<IEnumerable<TEntity>>(models);
    //     await _repository.CreateMany(entities, cancellationToken);
    //     return Ok();
    // }

    // [HttpPut]
    // public async Task<ActionResult> Put([FromBody] TModel model, CancellationToken cancellationToken = default)
    // {
    //     var entity = _mapper.Map<TEntity>(model);
    //     await _repository.Update(entity, cancellationToken);
    //     return Ok();
    // }

    // [HttpPut("many")]
    // public async Task<ActionResult> PutMany(IEnumerable<TModel> models, CancellationToken cancellationToken = default)
    // {
    //     var entities = _mapper.Map<IEnumerable<TEntity>>(models);
    //     await _repository.UpdateMany(entities, cancellationToken);
    //     return Ok();
    // }

    // [HttpGet("query")]
    // public async Task<ActionResult<IEnumerable<TModel>>> Query(string query, CancellationToken cancellationToken = default)
    // {
    //     var entities = await _repository.Query(query, cancellationToken);
    //     if (!entities.Any())
    //         return NotFound();

    //     var models = _mapper.Map<IEnumerable<TModel>>(entities);
    //     return Ok(models);
    // }
}
