using Devantler.DataMesh.DataProduct.Core.Base.Repositories;
using Devantler.DataMesh.DataProduct.Core.Entities;
using Devantler.DataMesh.DataProduct.DataStore.SQLite.Contexts;

namespace Devantler.DataMesh.DataProduct.DataStore.SQLite.Repositories;

public class StudentRepository : ICRUDRepository<StudentEntity>
{
    private readonly StudentDbContext studentDbContext;

    public StudentRepository(StudentDbContext studentDbContext)
    {
        this.studentDbContext = studentDbContext;
    }

    public void CreateAsync(StudentEntity entity, CancellationToken cancellationToken = default)
    {
        studentDbContext.Students.Add(entity);
        studentDbContext.SaveChanges();
    }

    public void DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var student = studentDbContext.Students.FirstOrDefault(s => s.Id == id);
        if (student == null)
            throw new Exception("Student not found");
        studentDbContext.Students.Remove(student);
        studentDbContext.SaveChanges();
    }
    public StudentEntity ReadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var student = studentDbContext.Students.FirstOrDefault(s => s.Id == id);
        if (student == null)
            throw new Exception("Student not found");
        return student;
    }
    public void UpdateAsync(StudentEntity entity, CancellationToken cancellationToken = default)
    {
        var student = studentDbContext.Students.FirstOrDefault(s => s.Id == entity.Id);
        if (student == null)
            throw new Exception("Student not found");
        studentDbContext.Students.Update(entity);
        studentDbContext.SaveChanges();
    }
}
