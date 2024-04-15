using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.CSharp.Model;
using Devantler.Commons.CodeGen.Mapping.Avro;
using Devantler.Commons.StringHelpers.Extensions;
using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Generator.Extensions;
using Devantler.DataProduct.Generator.Models;
using Microsoft.CodeAnalysis;

namespace Devantler.DataProduct.Generator.IncrementalGenerators;

/// <summary>
/// Generates AutoMapper profiles.
/// </summary>
[Generator]
public class AutoMapperProfileGenerator : GeneratorBase
{
  /// <summary>
  /// Generates AutoMapper profiles.
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
    var schemaRegistryClient = options.SchemaRegistry.CreateSchemaRegistryClient();
    var rootSchema = schemaRegistryClient.GetSchema(options.SchemaRegistry.Schema.Subject, options.SchemaRegistry.Schema.Version);

    var codeCompilation = new CSharpCompilation();

    var @class = new CSharpClass("AutoMapperProfile")
        .AddImport(new CSharpUsing("AutoMapper"))
        .AddImport(new CSharpUsing("Devantler.DataProduct.Features.Schemas"))
        .SetNamespace("Devantler.DataProduct")
        .SetDocBlock(new CSharpDocBlock("AutoMapper profile for mapping between models and entities."))
        .SetBaseClass(new CSharpClass("Profile"));

    var constructor = new CSharpConstructor("AutoMapperProfile")
        .SetDocBlock(new CSharpDocBlock("Creates a new instance of <see cref=\"AutoMapperProfile\"/>."));

    _ = @class.AddImport(new CSharpUsing("Devantler.DataProduct.Features.DataStore.Entities"));
    foreach (var schema in rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>())
    {
      string schemaName = schema.Name.ToPascalCase();
      _ = constructor.AddStatement($"_ = CreateMap<{schemaName}, {schemaName}Entity>().ReverseMap();");
    }

    _ = @class.AddConstructor(constructor);
    _ = codeCompilation.AddType(@class);

    var codeGenerator = new CSharpCodeGenerator();
    return codeGenerator.Generate(codeCompilation);
  }
}
