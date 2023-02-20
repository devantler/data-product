using System.Collections.Immutable;
using System.Text;
using Devantler.Commons.CodeGen.Core;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.Mapping.Avro.Mappers;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.DataStoreOptions;
using Devantler.DataMesh.DataProduct.Generator.Extensions;
using Devantler.DataMesh.DataProduct.Generator.Models;
using Devantler.DataMesh.SchemaRegistry;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

/// <summary>
/// A generator that generates Entity classes in the data product.
/// </summary>
[Generator]
public class EntitiesGenerator : GeneratorBase
{
    /// <inheritdoc/>
    public override void Generate(
        SourceProductionContext context,
        Compilation compilation,
        ImmutableArray<AdditionalFile> additionalFiles,
        DataProductOptions options)
    {
        if (options.DataStoreOptions.Type != DataStoreType.Relational)
            return;

        var schemaRegistryService = options.GetSchemaRegistryService();
        var rootSchema = schemaRegistryService.GetSchema(options.Schema.Subject, options.Schema.Version);

        var mapper = new AvroEntitiesCompilationMapper();

        var codeCompilation = mapper.Map(rootSchema, Language.CSharp);

        var generator = new CSharpCodeGenerator();
        foreach (var codeItem in generator.Generate(codeCompilation, options => options.NamespaceToUse = "Devantler.DataMesh.DataProduct.DataStore.Relational.Entities"))
        {
            string sourceText = codeItem.Value.AddMetadata(GetType());
            context.AddSource(codeItem.Key, SourceText.From(sourceText, Encoding.UTF8));
        }
    }
}