using AutoMapper;
using Devantler.DataMesh.DataProduct.Core.Base.Repositories;
using Devantler.DataMesh.DataProduct.Core.Base.Services;
using Devantler.DataMesh.DataProduct.Core.Entities;
using Devantler.DataMesh.DataProduct.Core.Models;

namespace Devantler.DataMesh.DataProduct.DataStore.SQLite.Services;

public class StudentService : ICRUDService<Student>
{
    private readonly ICRUDRepository<StudentEntity> _studentRepository;
    private readonly IMapper _mapper;

    public StudentService(ICRUDRepository<StudentEntity> studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }

    public Task CreateAsync(Student model, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<StudentEntity>(model);
        _studentRepository.CreateAsync(entity, cancellationToken);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _studentRepository.DeleteAsync(id, cancellationToken);
        return Task.CompletedTask;
    }
    public Task<Student> ReadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = _studentRepository.ReadAsync(id, cancellationToken);
        var model = _mapper.Map<Student>(entity);
        return Task.FromResult(model);
    }
    public Task UpdateAsync(Student model, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<StudentEntity>(model);
        _studentRepository.UpdateAsync(entity, cancellationToken);
        return Task.CompletedTask;
    }
}
