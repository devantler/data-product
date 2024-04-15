using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.Core;
using Devantler.Commons.CodeGen.Core.Model;
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
/// A generator for service registrations and usages for outputs.
/// </summary>
[Generator]
public class OutputsStartupExtensionsGenerator : GeneratorBase
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
    if (!options.FeatureFlags.EnableOutputs || !options.Outputs.Any())
      return [];

    var schemaRegistryClient = options.SchemaRegistry.CreateSchemaRegistryClient();
    var rootSchema = schemaRegistryClient.GetSchema(options.SchemaRegistry.Schema.Subject,
        options.SchemaRegistry.Schema.Version);

    var codeCompilation = new CSharpCompilation();

    var @class = new CSharpClass("OutputsStartupExtensions")
        .AddImport(new CSharpUsing("Devantler.DataProduct.Features.Outputs" + ".Services"))
        .AddImport(new CSharpUsing("Devantler.DataProduct.Configuration.Options.Outputs"))
        .AddImport(new CSharpUsing("Devantler.DataProduct.Features.Schemas"))
        .SetDocBlock(new CSharpDocBlock(
            "A class that contains extension methods for service registrations and usages for outputs"))
        .SetNamespace("Devantler.DataProduct.Features.Outputs")
        .SetIsStatic(true)
        .SetIsPartial(true);

    var servicesParameter = new CSharpParameter("IServiceCollection", "services");
    var optionsParameter = new CSharpParameter("List<OutputOptions>", "options");
    var addGeneratedServiceRegistrationsMethod = new CSharpMethod("AddGeneratedServiceRegistrations")
        .SetIsStatic(true)
        .SetIsPartial(true)
        .SetIsExtensionMethod(true)
        .SetDocBlock(new CSharpDocBlock("Adds generated service registrations for outputs."))
        .SetVisibility(Visibility.Private)
        .AddParameter(servicesParameter)
        .AddParameter(optionsParameter);

    var schemas = rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>();

    var avroSchemaParser = new AvroSchemaParser();

    foreach (var schema in schemas)
    {
      var schemaType = schema.Fields.FirstOrDefault(f => f.Name.Equals("id", StringComparison.OrdinalIgnoreCase))?.Type;
      string schemaIdType = schemaType is not null
          ? avroSchemaParser.Parse(schemaType, Language.CSharp)
          : "Guid";
      foreach (var outputOptions in options.Outputs)
      {
        _ = addGeneratedServiceRegistrationsMethod.AddStatement(
            $"_ = services.AddSingleton<IOutputService<{schemaIdType}, {schema.Name}>, {outputOptions.Type}OutputService<{schemaIdType}, {schema.Name}>>();"
        );
      }
    }

    _ = @class.AddMethod(addGeneratedServiceRegistrationsMethod);
    _ = codeCompilation.AddType(@class);

    var codeGenerator = new CSharpCodeGenerator();
    return codeGenerator.Generate(codeCompilation);
  }
}
