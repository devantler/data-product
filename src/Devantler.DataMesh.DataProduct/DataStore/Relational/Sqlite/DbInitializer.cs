// using Devantler.DataMesh.DataProduct.Models;

// namespace Devantler.DataMesh.DataProduct.DataStore.Relational.Sqlite;

// /// <summary>
// /// Initializes the sample data store with seed data.
// /// </summary>
// public static class DbInitializer
// {
//     /// <summary>
//     /// Initializes the sample data store with seed data.
//     /// </summary>
//     /// <param name="context"></param>
//     public static void Initialize(SqliteDbContext context)
//     {
//         // Look for any students.
//         if (context.Students.Any())
//         {
//             return; // DB has been seeded
//         }

//         var students = new[]
//         {
//             new Student { FirstMidName = "Carson", LastName = "Alexander" },//EnrollmentDate = DateTime.Parse("2019-09-01", CultureInfo.InvariantCulture)},
//             new Student { FirstMidName = "Meredith", LastName = "Alonso" },//EnrollmentDate = DateTime.Parse("2017-09-01", CultureInfo.InvariantCulture)},
//             new Student { FirstMidName = "Arturo", LastName = "Anand" },//EnrollmentDate = DateTime.Parse("2018-09-01", CultureInfo.InvariantCulture)},
//             new Student { FirstMidName = "Gytis", LastName = "Barzdukas" },//EnrollmentDate = DateTime.Parse("2017-09-01",CultureInfo.InvariantCulture)},
//             new Student { FirstMidName = "Yan", LastName = "Li" },// EnrollmentDate = DateTime.Parse("2017-09-01", CultureInfo.InvariantCulture) },
//             new Student { FirstMidName = "Peggy", LastName = "Justice" },// EnrollmentDate = DateTime.Parse("2016-09-01", CultureInfo.InvariantCulture) },
//             new Student { FirstMidName = "Laura", LastName = "Norman" },// EnrollmentDate = DateTime.Parse("2018-09-01", CultureInfo.InvariantCulture) },
//             new Student { FirstMidName = "Nino", LastName = "Olivetto" },// EnrollmentDate = DateTime.Parse("2019-09-01", CultureInfo.InvariantCulture) }
//         };

//         context.Students.AddRange(students);
//         _ = context.SaveChanges();

//         var courses = new[]
//         {
//             new Course { Id = 1050, Title = "Chemistry", Credits = 3 },
//             new Course { Id = 4022, Title = "Microeconomics", Credits = 3 },
//             new Course { Id = 4041, Title = "Macroeconomics", Credits = 3 },
//             new Course { Id = 1045, Title = "Calculus", Credits = 4 },
//             new Course { Id = 3141, Title = "Trigonometry", Credits = 4 },
//             new Course { Id = 2021, Title = "Composition", Credits = 3 },
//             new Course { Id = 2042, Title = "Literature", Credits = 4 }
//         };

//         context.Courses.AddRange(courses);
//         _ = context.SaveChanges();

//         var enrollments = new[]
//         {
//             new Enrollment { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Meredith")!, Course = context.Courses.FirstOrDefault(s => s.Id == 1050)!, Grade = Grade.A },
//             new Enrollment { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Meredith")!, Course = context.Courses.FirstOrDefault(s => s.Id == 4022)!, Grade = Grade.C },
//             new Enrollment { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Meredith")!, Course = context.Courses.FirstOrDefault(s => s.Id == 4041)!, Grade = Grade.B },
//             new Enrollment { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Arturo")!, Course = context.Courses.FirstOrDefault(s => s.Id == 1045)!, Grade = Grade.B },
//             new Enrollment { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Arturo")!, Course = context.Courses.FirstOrDefault(s => s.Id == 3141)!, Grade = Grade.F },
//             new Enrollment { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Arturo")!, Course = context.Courses.FirstOrDefault(s => s.Id == 2021)!, Grade = Grade.F },
//             new Enrollment { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Gytis")!, Course = context.Courses.FirstOrDefault(s => s.Id == 1050)! },
//             new Enrollment { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Yan")!, Course = context.Courses.FirstOrDefault(s => s.Id == 1050)! },
//             new Enrollment { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Yan")!, Course = context.Courses.FirstOrDefault(s => s.Id == 4022)!, Grade = Grade.F },
//             new Enrollment { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Peggy")!, Course = context.Courses.FirstOrDefault(s => s.Id == 4041)!, Grade = Grade.C },
//             new Enrollment { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Laura")!, Course = context.Courses.FirstOrDefault(s => s.Id == 1045)! },
//             new Enrollment { Student = context.Students.FirstOrDefault(s => s.FirstMidName == "Nino")!, Course = context.Courses.FirstOrDefault(s => s.Id == 3141)!, Grade = Grade.A },
//         };

//         context.Enrollments.AddRange(enrollments);
//         _ = context.SaveChanges();
//     }
// }
