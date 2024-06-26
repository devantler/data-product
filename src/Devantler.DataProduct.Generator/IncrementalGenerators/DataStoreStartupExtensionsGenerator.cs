using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.Core;
using Devantler.Commons.CodeGen.Core.Model;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.CSharp.Model;
using Devantler.Commons.CodeGen.Mapping.Avro;
using Devantler.Commons.StringHelpers.Extensions;
using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.DataStore;
using Devantler.DataProduct.Configuration.Options.DataStore.SQL;
using Devantler.DataProduct.Generator.Extensions;
using Devantler.DataProduct.Generator.Models;
using Microsoft.CodeAnalysis;

namespace Devantler.DataProduct.Generator.IncrementalGenerators;

/// <summary>
/// A generator for service registrations and usages for a data store.
/// </summary>
[Generator]
public class DataStoreStartupExtensionsGenerator : GeneratorBase
{
  /// <summary>
  /// Generates service registrations and usages for a data store.
  /// </summary>
  /// <param name="compilation"></param>
  /// <param name="additionalFiles"></param>
  /// <param name="options"></param>
  public override Dictionary<string, string> Generate(
      Compilation compilation,
      ImmutableArray<AdditionalFile> additionalFiles,
      DataProductOptions options)
  {
    var schemaRegistryClient = options.SchemaRegistry.CreateSchemaRegistryClient();
    var rootSchema = schemaRegistryClient.GetSchema(options.SchemaRegistry.Schema.Subject, options.SchemaRegistry.Schema.Version);

    var codeCompilation = new CSharpCompilation();

    string codeNamespace = "Devantler.DataProduct.Features.DataStore";
    string dataStoreOptionsNamespace = "Devantler.DataProduct.Configuration.Options.DataStore";
    var @class = new CSharpClass("DataStoreStartupExtensions")
        .AddImport(new CSharpUsing(string.IsNullOrEmpty(dataStoreOptionsNamespace) ? "Devantler.DataProduct.Configuration.Options.DataStore" : dataStoreOptionsNamespace))
        .AddImport(new CSharpUsing("Devantler.DataProduct.Features.DataStore.Services"))
        .AddImport(new CSharpUsing("Devantler.DataProduct.Features.DataStore.Repositories"))
        .AddImport(new CSharpUsing("Devantler.DataProduct.Features.DataStore.Entities"))
        .AddImport(new CSharpUsing("Devantler.DataProduct.Features.Schemas"))
        .AddImport(new CSharpUsing("Microsoft.EntityFrameworkCore"))
        .SetDocBlock(new CSharpDocBlock("A class that contains extension methods for service registrations and usages for a data store."))
        .SetNamespace(codeNamespace)
        .SetIsStatic(true)
        .SetIsPartial(true);

    var servicesParameter = new CSharpParameter("IServiceCollection", "services");
    var optionsParameter = new CSharpParameter("DataStoreOptions", "options");
    var addGeneratedServiceRegistrationsMethod = new CSharpMethod("AddGeneratedServiceRegistrations")
        .SetIsStatic(true)
        .SetIsPartial(true)
        .SetIsExtensionMethod(true)
        .SetDocBlock(new CSharpDocBlock("Adds generated service registrations for a data store."))
        .SetVisibility(Visibility.Private)
        .AddParameter(servicesParameter)
        .AddParameter(optionsParameter);

    var avroSchemaParser = new AvroSchemaParser();

    switch (options.DataStore.Type)
    {
      case DataStoreType.SQL:
        string providerName = options.DataStore.Provider switch
        {
          SQLDataStoreProvider.Sqlite => "Sqlite",
          SQLDataStoreProvider.PostgreSQL => "Npgsql",
          _ => throw new NotSupportedException($"The data store provider '{options.DataStore.Provider}' is not supported.")
        };
        _ = addGeneratedServiceRegistrationsMethod.AddStatement($"_ = services.AddPooledDbContextFactory<{options.DataStore.Provider}DbContext>(dbOptions => dbOptions.UseLazyLoadingProxies().Use{providerName}(options?.ConnectionString));");
        foreach (var schema in rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>())
        {
          string schemaName = schema.Name.ToPascalCase();
          var schemaType = schema.Fields.FirstOrDefault(f => f.Name.Equals("id", StringComparison.OrdinalIgnoreCase))?.Type;
          string schemaIdType = schemaType is not null
              ? avroSchemaParser.Parse(schemaType, Language.CSharp)
              : "Guid";
          _ = addGeneratedServiceRegistrationsMethod
              .AddStatement($"_ = services.AddScoped<IRepository<{schemaIdType}, {schemaName}Entity>, {schemaName}Repository>();")
              .AddStatement($"_ = services.AddScoped<IDataStoreService<{schemaIdType}, {schemaName}>, {schemaName}DataStoreService>();");
        }
        _ = addGeneratedServiceRegistrationsMethod
            .AddStatement("using var scope = services.BuildServiceProvider().CreateScope();")
            .AddStatement($"var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<{options.DataStore.Provider}DbContext>>();")
            .AddStatement("var context = contextFactory.CreateDbContext();")
            .AddStatement("_ = context.Database.EnsureCreated();");
        break;
      case DataStoreType.NoSQL:
        throw new NotSupportedException("Document based data stores are not supported yet.");
      case DataStoreType.Graph:
        throw new NotSupportedException("Graph based data stores are not supported yet.");
      default:
        throw new NotSupportedException($"Data store type '{options.DataStore.Type}' is not supported.");
    }

    _ = @class.AddMethod(addGeneratedServiceRegistrationsMethod);

    _ = codeCompilation.AddType(@class);

    var codeGenerator = new CSharpCodeGenerator();
    return codeGenerator.Generate(codeCompilation);
  }
}
