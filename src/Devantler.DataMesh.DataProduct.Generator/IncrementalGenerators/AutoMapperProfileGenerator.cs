using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.CSharp.Model;
using Devantler.Commons.CodeGen.Mapping.Avro;
using Devantler.Commons.StringHelpers;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Generator.Models;
using Devantler.DataMesh.SchemaRegistry;
using Microsoft.CodeAnalysis;

namespace Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

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
        var schemaRegistryService = options.Services.SchemaRegistry.CreateSchemaRegistryService();
        var rootSchema = schemaRegistryService.GetSchema(options.Services.SchemaRegistry.Schema.Subject, options.Services.SchemaRegistry.Schema.Version);

        var codeCompilation = new CSharpCompilation();

        var @class = new CSharpClass("AutoMapperProfile")
            .AddImport(new CSharpUsing("AutoMapper"))
            .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IModel")))
            .SetNamespace(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "StartupExtensions"))
            .SetDocBlock(new CSharpDocBlock("AutoMapper profile for mapping between models and entities."))
            .SetBaseClass(new CSharpClass("Profile"));

        var constructor = new CSharpConstructor("AutoMapperProfile")
            .SetDocBlock(new CSharpDocBlock("Creates a new instance of <see cref=\"AutoMapperProfile\"/>."));

        _ = @class.AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IEntity")));
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
