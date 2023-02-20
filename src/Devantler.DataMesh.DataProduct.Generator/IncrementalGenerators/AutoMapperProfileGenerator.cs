using System.Collections.Immutable;
using System.Text;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.CSharp.Model;
using Devantler.Commons.CodeGen.Mapping.Avro.Extensions;
using Devantler.Commons.StringHelpers;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.DataStoreOptions;
using Devantler.DataMesh.DataProduct.Generator.Extensions;
using Devantler.DataMesh.DataProduct.Generator.Models;
using Devantler.DataMesh.SchemaRegistry;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

/// <summary>
/// Generates AutoMapper profiles.
/// </summary>
[Generator]
public class AutoMapperProfileGenerator : GeneratorBase
{
    public override void Generate(
        SourceProductionContext context,
        Compilation compilation,
        ImmutableArray<AdditionalFile> additionalFiles,
        DataProductOptions options
    )
    {
        var schemaRegistryService = options.GetSchemaRegistryService();
        var rootSchema = schemaRegistryService.GetSchema(options.Schema.Subject, options.Schema.Version);

        var codeCompilation = new CSharpCompilation();

        var @class = new CSharpClass("AutoMapperProfile")
            .AddImport(new CSharpUsing("AutoMapper"))
            .AddImport(new CSharpUsing("Devantler.DataMesh.DataProduct.Models"))
            .SetNamespace("Devantler.DataMesh.DataProduct")
            .SetDocBlock(new CSharpDocBlock("AutoMapper profile for mapping between models and entities."))
            .SetBaseClass(new CSharpClass("Profile"));

        var constructor = new CSharpConstructor("AutoMapperProfile");

        if (options.DataStoreOptions.Type == DataStoreType.Relational)
        {
            _ = @class.AddImport(new CSharpUsing("Devantler.DataMesh.DataProduct.DataStore.Relational.Entities"));

            foreach (var schema in rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>())
            {
                string schemaName = schema.Name.ToPascalCase();
                _ = constructor.AddStatement($"_ = CreateMap<{schemaName}, {schemaName}Entity>().ReverseMap();");
            }
        }

        _ = @class.AddConstructor(constructor);
        _ = codeCompilation.AddType(@class);

        string sourceText = codeCompilation.Compile().First().Value.AddMetadata(GetType());
        context.AddSource("AutoMapperProfile.g.cs", SourceText.From(sourceText, Encoding.UTF8));
    }
}
