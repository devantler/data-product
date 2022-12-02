using AutoMapper;
using Devantler.DataMesh.DataProduct.Features.DataStores.Base;
using Devantler.DataMesh.DataProduct.Features.DataStores.Entities;
using Devantler.DataMesh.DataProduct.Models;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Features.Apis.REST;

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

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        await _repository.Delete(id, cancellationToken);
        return Ok();
    }

    [HttpDelete("Bulk")]
    public async Task<ActionResult> DeleteBulk(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteBulk(ids, cancellationToken);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TModel>> Get(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.Read(id, cancellationToken);
        if (entity == null)
        {
            return NotFound();
        }
        var model = _mapper.Map<TModel>(entity);
        return Ok(model);
    }

    [HttpGet("Bulk")]
    public async Task<ActionResult<IEnumerable<TModel>>> GetBulk(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var entities = await _repository.ReadBulk(ids, cancellationToken);
        if (entities.Any() is false)
        {
            return NotFound();
        }
        var models = _mapper.Map<IEnumerable<TModel>>(entities);
        return Ok(models);
    }

    [HttpGet("Paged")]
    public async Task<ActionResult<IEnumerable<TModel>>> GetPaged(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var entities = await _repository.ReadPaged(page, pageSize, cancellationToken);
        if (entities.Any() is false)
        {
            return NotFound();
        }
        var models = _mapper.Map<IEnumerable<TModel>>(entities);
        return Ok(models);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Post(TModel model, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TEntity>(model);
        var id = await _repository.Create(entity, cancellationToken);
        if (id == Guid.Empty)
        {
            return BadRequest();
        }
        return Ok(id);
    }

    [HttpPost("Bulk")]
    public async Task<ActionResult> PostBulk(IEnumerable<TModel> models, CancellationToken cancellationToken = default)
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(models);
        await _repository.CreateBulk(entities, cancellationToken);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> Put(TModel model, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TEntity>(model);
        await _repository.Update(entity, cancellationToken);
        return Ok();
    }

    [HttpPut("Bulk")]
    public async Task<ActionResult> PutBulk(IEnumerable<TModel> models, CancellationToken cancellationToken = default)
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(models);
        await _repository.UpdateBulk(entities, cancellationToken);
        return Ok();
    }

    [HttpGet("Query")]
    public async Task<ActionResult<IEnumerable<TModel>>> Query(string query, CancellationToken cancellationToken = default)
    {
        var entities = await _repository.Query(query, cancellationToken);
        if (entities.Any() is false)
        {
            return NotFound();
        }
        var models = _mapper.Map<IEnumerable<TModel>>(entities);
        return Ok(models);
    }
}
