using System.Reflection;
using Devantler.DataMesh.DataProduct.Core.Base.Repositories;
using Devantler.DataMesh.DataProduct.Core.Base.Services;
using Devantler.DataMesh.DataProduct.Core.Entities;
using Devantler.DataMesh.DataProduct.Core.Models;
using Devantler.DataMesh.DataProduct.DataStore.SQLite.Contexts;
using Devantler.DataMesh.DataProduct.DataStore.SQLite.Repositories;
using Devantler.DataMesh.DataProduct.DataStore.SQLite.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Devantler.DataMesh.DataProduct.DataStore.SQLite;

public static partial class StartupExtensions
{
    public static IServiceCollection AddSQLiteService(this IServiceCollection services)
    {
        var coreAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "Devantler.DataMesh.DataProduct.Core");
        services.AddAutoMapper(coreAssembly);
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v0.0.1", new OpenApiInfo
            {
                Version = "v0.0.1",
                Title = "Students API",
                Description = "An ASP.NET Core Web API to query and manage students and courses at SDU.",
                TermsOfService = new Uri("https://policies.google.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Nikolai Emil Damm",
                    Email = "nikolaiemildamm@icloud.com",
                    Url = new Uri("https://devantler.com"),
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
        services.AddScoped<ICRUDService<Core.Models.Student>, StudentService>();
        services.AddScoped<ICRUDRepository<Core.Entities.Student>, StudentRepository>();
        services.AddDbContext<StudentDbContext>();
        //TODO: Do this when my code works
        // services.AddServiceFromSourceOutput();
        // services.AddRepositoryFromSourceOutput();
        // services.AddDbContextFromSourceOutput();
        return services;
    }

    public static WebApplication UseSQLiteService(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint($"/swagger/v0.0.1/swagger.json", "Students API v0.0.1");
                }
            );
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        return app;
    }

    static partial void AddServiceFromSourceOutput(this IServiceCollection services);
    static partial void AddRepositoryFromSourceOutput(this IServiceCollection services);
    static partial void AddDbContextFromSourceOutput(this IServiceCollection services);
}
