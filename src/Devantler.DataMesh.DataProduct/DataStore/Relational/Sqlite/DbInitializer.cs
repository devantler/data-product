using Devantler.DataMesh.DataProduct.DataStore.Relational.Entities;

namespace Devantler.DataMesh.DataProduct.DataStore.Relational.Sqlite;

/// <summary>
/// Initializes the sample data store with seed data.
/// </summary>
public static class DbInitializer
{
    /// <summary>
    /// Initializes the sample data store with seed data.
    /// </summary>
    /// <param name="context"></param>
    public static void Initialize(SqliteDbContext context)
    {
        // Look for any students.
        if (context.Students.Any())
        {
            return; // DB has been seeded
        }

        var students = new[]
        {
            new StudentEntity { FirstMidName = "Carson", LastName = "Alexander" },//EnrollmentDate = DateTime.Parse("2019-09-01", CultureInfo.InvariantCulture)},
            new StudentEntity { FirstMidName = "Meredith", LastName = "Alonso" },//EnrollmentDate = DateTime.Parse("2017-09-01", CultureInfo.InvariantCulture)},
            new StudentEntity { FirstMidName = "Arturo", LastName = "Anand" },//EnrollmentDate = DateTime.Parse("2018-09-01", CultureInfo.InvariantCulture)},
            new StudentEntity { FirstMidName = "Gytis", LastName = "Barzdukas" },//EnrollmentDate = DateTime.Parse("2017-09-01",CultureInfo.InvariantCulture)},
            new StudentEntity { FirstMidName = "Yan", LastName = "Li" },// EnrollmentDate = DateTime.Parse("2017-09-01", CultureInfo.InvariantCulture) },
            new StudentEntity { FirstMidName = "Peggy", LastName = "Justice" },// EnrollmentDate = DateTime.Parse("2016-09-01", CultureInfo.InvariantCulture) },
            new StudentEntity { FirstMidName = "Laura", LastName = "Norman" },// EnrollmentDate = DateTime.Parse("2018-09-01", CultureInfo.InvariantCulture) },
            new StudentEntity { FirstMidName = "Nino", LastName = "Olivetto" },// EnrollmentDate = DateTime.Parse("2019-09-01", CultureInfo.InvariantCulture) }
        };

        context.Students.AddRange(students);
        _ = context.SaveChanges();

        // var enrollments = new[]
        // {
        //     new EnrollmentEntity { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Meredith")! },
        //     new EnrollmentEntity { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Meredith")! },
        //     new EnrollmentEntity { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Meredith")! },
        //     new EnrollmentEntity { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Arturo")! },
        //     new EnrollmentEntity { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Arturo")! },
        //     new EnrollmentEntity { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Arturo")! },
        //     new EnrollmentEntity { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Gytis")! },
        //     new EnrollmentEntity { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Yan")! },
        //     new EnrollmentEntity { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Yan")! },
        //     new EnrollmentEntity { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Peggy")! },
        //     new EnrollmentEntity { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Laura")! },
        //     new EnrollmentEntity { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Nino")! },
        // };

        // context.Enrollments.AddRange(enrollments);
        // _ = context.SaveChanges();
    }
}
