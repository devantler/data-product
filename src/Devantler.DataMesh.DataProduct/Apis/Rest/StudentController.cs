using AutoMapper;
using Devantler.DataMesh.DataProduct.DataStore.Relational.Entities;
using Devantler.DataMesh.DataProduct.DataStore.Relational.Repositories;
using Devantler.DataMesh.DataProduct.Models;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Apis.Rest;

//TODO: Generate with RestApiControllerGenerator
/// <summary>
/// A controller to handle REST API requests for the <see cref="Student"/> model.
/// </summary>
[ApiController]
[Route("[controller]")]
public sealed class StudentController : ControllerBase, IController<Student>
{
    readonly EntityFrameworkRepository<StudentEntity> _repository;
    readonly IMapper _mapper;

    /// <summary>
    /// Creates a new instance of the <see cref="StudentController"/> class.
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="mapper"></param>
    public StudentController(EntityFrameworkRepository<StudentEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Reads a Student by its <paramref name="id"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    [HttpGet]
    public async Task<ActionResult<Student>> Read(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.ReadAsync(id, cancellationToken);
        var result = _mapper.Map<StudentEntity, Student>(entity);
        return Ok(result);
    }

    /// <summary>
    /// Creates a new Student.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost]
    public async Task<ActionResult<Student>> Create(Student model, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<Student, StudentEntity>(model);
        var createdEntity = await _repository.CreateAsync(entity, cancellationToken);
        var result = _mapper.Map<StudentEntity, Student>(createdEntity);
        return Ok(result);
    }

    /// <summary>
    /// Updates an existing Student.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPut]
    public async Task<ActionResult<Student>> Update(Student model, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<Student, StudentEntity>(model);
        var updatedEntity = await _repository.UpdateAsync(entity, cancellationToken);
        var result = _mapper.Map<StudentEntity, Student>(updatedEntity);
        return Ok(result);
    }

    /// <summary>
    /// Deletes an existing Student.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="NotImplementedException"></exception>
    [HttpDelete]
    public async Task<ActionResult<Student>> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var deletedEntity = await _repository.DeleteAsync(id, cancellationToken);
        var result = _mapper.Map<StudentEntity, Student>(deletedEntity);
        return Ok(result);
    }
}
