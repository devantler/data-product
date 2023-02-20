using Devantler.DataMesh.DataProduct.DataStore.Relational.Entities;
using Devantler.DataMesh.DataProduct.DataStore.Relational.Sqlite;

namespace Devantler.DataMesh.DataProduct.DataStore.Relational.Repositories;

//TODO: Generate with RelationalRepositoryGenerator
/// <summary>
/// A repository for <see cref="StudentEntity"/> entities.
/// </summary>
public class StudentRepository : EntityFrameworkRepository<StudentEntity>
{
    /// <inheritdoc />
    public StudentRepository(SqliteDbContext context) : base(context)
    {
    }
}
