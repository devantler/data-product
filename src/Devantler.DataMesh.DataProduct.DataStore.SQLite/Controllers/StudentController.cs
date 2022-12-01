using Devantler.DataMesh.DataProduct.DataStore.SQLite.Services;
using Microsoft.AspNetCore.Mvc;
using Devantler.DataMesh.DataProduct.Core.Models;
using Devantler.DataMesh.DataProduct.Core.Base.Controllers;
using Devantler.DataMesh.DataProduct.Core.Base.Services;
using AutoMapper;

namespace Devantler.DataMesh.DataProduct.DataStore.SQLite.Controllers;


public class StudentController : ApiController, IHTTPController<Student>
{
    private readonly ICRUDService<Student> _studentService;
    private readonly IMapper _mapper;

    public StudentController(StudentService studentService, IMapper mapper) {
        _studentService = studentService;
        _mapper = mapper;
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        await _studentService.DeleteAsync(id, cancellationToken);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<Student>> Get(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _studentService.ReadAsync(id, cancellationToken);
        if (result is null)
            return NotFound();
        return Ok(result);

    }

    [HttpPost] 
    public async Task<ActionResult> Post(Student model, CancellationToken cancellationToken = default) {
        var domainModel = _mapper.Map<Student>(model);
        await _studentService.CreateAsync(domainModel, cancellationToken);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> Put(Student model, CancellationToken cancellationToken = default) {
        var domainModel = _mapper.Map<Student>(model);
        await _studentService.UpdateAsync(domainModel, cancellationToken);
        return Ok();
    }
}
