using System.Collections.Immutable;
using System.Text;
using Devantler.Commons.CodeGen.Core;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.Mapping.Avro.Mappers;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Generator.Extensions;
using Devantler.DataMesh.DataProduct.Generator.Models;
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
    public override Dictionary<string, string> Generate(
        Compilation compilation,
        ImmutableArray<AdditionalFile> additionalFiles,
        DataProductOptions options)
    {
        var schemaRegistryService = options.GetSchemaRegistryService();
        var rootSchema = schemaRegistryService.GetSchema(options.Schema.Subject, options.Schema.Version);

        var mapper = new AvroModelsCompilationMapper();

        var codeCompilation = mapper.Map(rootSchema, Language.CSharp);

        var generator = new CSharpCodeGenerator();
        return generator.Generate(codeCompilation, options => options.NamespaceToUse = "Devantler.DataMesh.DataProduct.Models");
    }
}
