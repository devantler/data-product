using Devantler.DataMesh.DataProduct.Core.Base.Repositories;
using Devantler.DataMesh.DataProduct.Core.Base.Services;
using Devantler.DataMesh.DataProduct.Core.Entities;
using Devantler.DataMesh.DataProduct.Core.Models;
using Devantler.DataMesh.DataProduct.DataStore.SQLite.Contexts;
using Devantler.DataMesh.DataProduct.DataStore.SQLite.Repositories;
using Devantler.DataMesh.DataProduct.DataStore.SQLite.Services;
using Microsoft.EntityFrameworkCore;

namespace Devantler.DataMesh.DataProduct.DataStore.SQLite;

public static partial class StartupExtensions
{
    public static IServiceCollection AddSQLiteService(this IServiceCollection services)
    {
        var coreAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "Devantler.DataMesh.DataProduct.Core");
        services.AddAutoMapper(coreAssembly);
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddScoped<ICRUDService<Student>, StudentService>();
        services.AddScoped<ICRUDRepository<StudentEntity>, StudentRepository>();
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
            app.UseSwaggerUI();
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
