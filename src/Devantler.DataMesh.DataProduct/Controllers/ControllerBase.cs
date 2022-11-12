using Devantler.DataMesh.DataProduct.Extensions;
using Devantler.DataMesh.DataProduct.Models;
using Devantler.DataMesh.DataProduct.Services;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace Devantler.DataMesh.DataProduct.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class ControllerBase<T> : ControllerBase, IController<T> where T : Model
{
    private readonly IService<T> _service;
    private readonly IValidator<T> _validator;
    private readonly ILogger _logger;

    protected ControllerBase(IService<T> service, IValidator<T> validator, ILogger logger)
    {
        _service = service;
        _validator = validator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<T>>> Query(string query, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _service.Query(query, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Query Error", typeof(T).Name);
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<T>>> GetPaged(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _service.ReadPagedAsync(page, pageSize, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetPaged Error", typeof(T).Name);
            return BadRequest(ex);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<T>> Get(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _service.ReadAsync(id, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get Error", typeof(T).Name);
            return BadRequest(ex);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post(T entity, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(entity, cancellationToken);

        if(!validationResult.IsValid) {
            validationResult.AddToModelState(ModelState);
            return BadRequest(ModelState);
        }

        try {
            await _service.CreateAsync(entity, cancellationToken);
            return Ok();
        } 
        catch (Exception ex)
        {
            _logger.LogError(ex, "Post Error", typeof(T).Name);
            return BadRequest(ex);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(T entity, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(entity, cancellationToken);

        if(!validationResult.IsValid) {
            validationResult.AddToModelState(ModelState);
            return BadRequest(ModelState);
        }

        try
        {
            await _service.UpdateAsync(entity, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Put Error", typeof(T).Name);
            return BadRequest(ex);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            await _service.DeleteAsync(id, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete Error", typeof(T).Name);
            return BadRequest(ex);
        }
    }
}
