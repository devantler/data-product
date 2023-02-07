using System.Collections.Immutable;
using System.Text;
using Devantler.Commons.CodeGen.Core;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.Mapping.Avro.Mappers;
using Devantler.DataMesh.DataProduct.Configuration.Extensions;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Generator.Extensions;
using Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators.Base;
using Devantler.DataMesh.SchemaRegistry;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

/// <summary>
/// A generator that generates Model classes in the data product.
/// </summary>
[Generator]
public class ModelsGenerator : GeneratorBase
{
    /// <inheritdoc/>
    public override void Generate(
        SourceProductionContext context,
        Compilation compilation,
        ImmutableArray<AdditionalFile> additionalFiles,
        DataProductOptions options)
    {
        //Hack to set the path to the local schema registry when in a source generator.
        options.SchemaRegistryOptions.OverrideLocalSchemaRegistryPath(additionalFiles.FirstOrDefault(x => x.FileName.EndsWith(".avsc"))?.FileDirectoryPath);
        var schemaRegistryService = options.GetSchemaRegistryService();
        var rootSchema = schemaRegistryService.GetSchemaAsync(options.Schema.Subject, options.Schema.Version).Result;

        var mapper = new AvroModelsCompilationMapper();

        var codeCompilation = mapper.Map(rootSchema, Language.CSharp);

        var generator = new CSharpCodeGenerator();
        foreach (var codeItem in generator.Generate(codeCompilation, options => options.NamespaceToUse = "Devantler.DataMesh.DataProduct.Models"))
        {
            string sourceText = codeItem.Value.AddMetadata();
            context.AddSource(codeItem.Key, SourceText.From(sourceText, Encoding.UTF8));
        }
    }
}
