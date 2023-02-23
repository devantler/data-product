using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.Core.Model;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.CSharp.Model;
using Devantler.Commons.CodeGen.Mapping.Avro.Extensions;
using Devantler.Commons.StringHelpers;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.DataStoreOptions;
using Devantler.DataMesh.DataProduct.Configuration.Options.DataStoreOptions.Relational;
using Devantler.DataMesh.DataProduct.Generator.Models;
using Devantler.DataMesh.SchemaRegistry;
using Microsoft.CodeAnalysis;

namespace Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

[Generator]
public class RelationalDataStoreStartupExtensionsGenerator : GeneratorBase
{
    /// <summary>
    /// Generates service registrations and usages for a relational data store.
    /// </summary>
    /// <param name="compilation"></param>
    /// <param name="additionalFiles"></param>
    /// <param name="options"></param>
    public override Dictionary<string, string> Generate(
        Compilation compilation,
        ImmutableArray<AdditionalFile> additionalFiles,
        DataProductOptions options)
    {
        var schemaRegistryService = options.GetSchemaRegistryService();
        var rootSchema = schemaRegistryService.GetSchema(options.Schema.Subject, options.Schema.Version);

        var codeCompilation = new CSharpCompilation();

        string codeNamespace = NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "RelationalDataStoreStartupExtensions");
        var @class = new CSharpClass("RelationalDataStoreStartupExtensions")
            .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "RelationalDataStoreOptionsBase")))
            .AddImport(new CSharpUsing("Microsoft.EntityFrameworkCore"))
            .SetNamespace(codeNamespace)
            .SetIsStatic(true)
            .SetIsPartial(true);

        var servicesParameter = new CSharpParameter("IServiceCollection", "services");
        var optionsParameter = new CSharpParameter("RelationalDataStoreOptionsBase", "options")
            .SetIsNullable(true);
        var addGeneratedServiceRegistrationsMethod = new CSharpMethod("AddGeneratedServiceRegistrations")
            .SetIsStatic(true)
            .SetIsPartial(true)
            .SetIsExtensionMethod(true)
            .SetVisibility(Visibility.Private)
            .AddParameter(servicesParameter)
            .AddParameter(optionsParameter);

        var appParameter = new CSharpParameter("WebApplication", "app");
        var useGeneratedServiceRegistrations = new CSharpMethod("UseGeneratedServiceRegistrations")
            .SetIsStatic(true)
            .SetIsPartial(true)
            .SetIsExtensionMethod(true)
            .SetVisibility(Visibility.Private)
            .AddParameter(appParameter)
            .AddParameter(optionsParameter);

        switch (options.DataStoreOptions.Type)
        {
            case DataStoreType.Relational:
                if (options.DataStoreOptions is not RelationalDataStoreOptionsBase relationalDataStoreOptions)
                    throw new InvalidOperationException("Relational data store options are not set.");
                switch (relationalDataStoreOptions.Provider)
                {
                    case RelationalDataStoreProvider.SQLite:
                        _ = addGeneratedServiceRegistrationsMethod
                            .AddStatement("_ = services.AddDbContext<SqliteDbContext>(dbOptions => dbOptions.UseSqlite(options?.ConnectionString));");

                        foreach (var schema in rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>())
                        {
                            string schemaName = schema.Name.ToPascalCase();
                            _ = addGeneratedServiceRegistrationsMethod
                                .AddStatement($"_ = services.AddScoped<EntityFrameworkRepository<{schemaName}Entity>, {schemaName}Repository>();");
                        }
                        _ = useGeneratedServiceRegistrations
                            .AddStatement("using var scope = app.Services.CreateScope();")
                            .AddStatement("var services = scope.ServiceProvider;")
                            .AddStatement("var context = services.GetRequiredService<SqliteDbContext>();")
                            .AddStatement("_ = context.Database.EnsureCreated();");
                        break;
                    default:
                        throw new NotSupportedException($"Relational data store provider '{relationalDataStoreOptions.Provider}' is not supported.");
                }
                break;
            case DataStoreType.DocumentBased:
                throw new NotSupportedException("Document based data stores are not supported yet.");
            case DataStoreType.GraphBased:
                throw new NotSupportedException("Graph based data stores are not supported yet.");
            default:
                throw new NotSupportedException($"Data store type '{options.DataStoreOptions.Type}' is not supported.");
        }

        _ = @class.AddMethod(addGeneratedServiceRegistrationsMethod);
        _ = @class.AddMethod(useGeneratedServiceRegistrations);

        _ = codeCompilation.AddType(@class);

        var codeGenerator = new CSharpCodeGenerator();
        return codeGenerator.Generate(codeCompilation);
    }
}
