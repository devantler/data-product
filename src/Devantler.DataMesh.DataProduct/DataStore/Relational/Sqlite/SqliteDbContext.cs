//TODO: Generate this class with a source generator
#pragma warning disable 1591
using Devantler.DataMesh.DataProduct.Models;
using Microsoft.EntityFrameworkCore;

namespace Devantler.DataMesh.DataProduct.DataStore.Relational.Sqlite;

public class SqliteDbContext : DbContext
{
    public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
    {
    }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<Course> Courses => Set<Course>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Course>().ToTable("Course");
        _ = modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
        _ = modelBuilder.Entity<Student>().ToTable("Student");
    }
}
#pragma warning restore 1591
