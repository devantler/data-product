using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.Core.Model;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.CSharp.Model;
using Devantler.Commons.CodeGen.Mapping.Avro;
using Devantler.Commons.StringHelpers.Extensions;
using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.DataStore;
using Devantler.DataProduct.Generator.Extensions;
using Devantler.DataProduct.Generator.Models;
using Microsoft.CodeAnalysis;

namespace Devantler.DataProduct.Generator.IncrementalGenerators;

/// <summary>
/// A generator that generates a Sqlite database context.
/// </summary>
[Generator]
public class DbContextGenerator : GeneratorBase
{
  /// <summary>
  /// A method to generate a Sqlite database context.
  /// </summary>
  /// <param name="compilation"></param>
  /// <param name="additionalFiles"></param>
  /// <param name="options"></param>
  public override Dictionary<string, string> Generate(
      Compilation compilation,
      ImmutableArray<AdditionalFile> additionalFiles,
      DataProductOptions options
  )
  {
    if (options.DataStore.Type != DataStoreType.SQL)
      return [];

    var dataStoreOptions = options.DataStore;

    var schemaRegistryClient = options.SchemaRegistry.CreateSchemaRegistryClient();
    var rootSchema = schemaRegistryClient.GetSchema(options.SchemaRegistry.Schema.Subject, options.SchemaRegistry.Schema.Version);

    var codeCompilation = new CSharpCompilation();

    var @class = new CSharpClass($"{dataStoreOptions.Provider}DbContext")
        .AddImport(new CSharpUsing("Microsoft.EntityFrameworkCore"))
        .AddImport(new CSharpUsing("Devantler.DataProduct.Features.DataStore.Entities"))
        .SetNamespace("Devantler.DataProduct.Features.DataStore")
        .SetDocBlock(new CSharpDocBlock($"A {dataStoreOptions.Provider} database context."))
        .SetBaseClass(new CSharpClass("DbContext"));

    var constructor = new CSharpConstructor(@class.Name)
        .SetDocBlock(new CSharpDocBlock($"A constructor to construct a {dataStoreOptions.Provider} database context."))
        .AddParameter(new CSharpConstructorParameter($"DbContextOptions<{@class.Name}>", "options")
            .SetIsBaseParameter(true));
    _ = @class.AddConstructor(constructor);

    var onModelCreatingMethod = new CSharpMethod("OnModelCreating")
        .SetDocBlock(new CSharpDocBlock("A method to configure the schema."))
        .AddParameter(new CSharpParameter("ModelBuilder", "modelBuilder"))
        .SetVisibility(Visibility.Protected)
        .SetIsOverride(true);

    foreach (var schema in rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>())
    {
      string schemaName = schema.Name.ToPascalCase();
      _ = @class.AddProperty(new CSharpProperty($"DbSet<{schemaName}Entity>", schemaName.ToPlural())
          .SetDocBlock(new CSharpDocBlock($"A property to access the {schemaName.ToKebabCase()} table."))
          .SetValue($"Set<{schemaName}Entity>()")
          .SetIsExpressionBodiedMember(true)
      );
      _ = onModelCreatingMethod.AddStatement(
          $"_ = modelBuilder.Entity<{schemaName}Entity>().ToTable(\"{schemaName}\");");
    }
    _ = @class.AddMethod(onModelCreatingMethod);

    _ = codeCompilation.AddType(@class);

    var codeGenerator = new CSharpCodeGenerator();
    return codeGenerator.Generate(codeCompilation);
  }
}
