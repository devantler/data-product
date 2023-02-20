using System.Collections.Immutable;
using System.Text;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.CSharp;
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
/// A generator that generates REST API controllers in the data product.
/// </summary>
[Generator]
public class RestApiControllerGenerator : GeneratorBase
{
    /// <summary>
    /// Generates REST API controllers in the data product.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="compilation"></param>
    /// <param name="additionalFiles"></param>
    /// <param name="options"></param>
    public override void Generate(
        SourceProductionContext context,
        Compilation compilation,
        ImmutableArray<AdditionalFile> additionalFiles,
        DataProductOptions options)
    {
        var schemaRegistryService = options.GetSchemaRegistryService();
        var rootSchema = schemaRegistryService.GetSchema(options.Schema.Subject, options.Schema.Version);

        var codeCompilation = new CSharpCompilation();

        foreach (var schema in rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>())
        {
            string schemaName = schema.Name.ToPascalCase();
            var @class = new CSharpClass($"{schemaName}Controller")
                .AddImport(new CSharpUsing("AutoMapper"))
                .AddImport(new CSharpUsing("Devantler.DataMesh.DataProduct.Models"))
                .SetNamespace("Devantler.DataMesh.DataProduct.Apis.Rest")
                .SetDocBlock(new CSharpDocBlock(
                    $$"""A controller to handle REST API requests for a the <see cref="{{schemaName}}" /> model."""))
                .SetBaseClass(new CSharpClass($"RestApiController<{schemaName}, {schemaName}Entity>"));


            var constructor = new CSharpConstructor(@class.Name)
                .SetDocBlock(new CSharpDocBlock($$"""Creates a new instance of <see cref="{{@class.Name}}" />"""));

            if (options.DataStoreOptions.Type == DataStoreType.Relational)
            {
                var repositoryConstructorParameter =
                    new CSharpConstructorParameter($"EntityFrameworkRepository<{schemaName}Entity>", "repository")
                        .SetIsBaseParameter(true);
                var mapperParameter = new CSharpConstructorParameter("IMapper", "mapper").SetIsBaseParameter(true);

                _ = constructor.AddParameter(repositoryConstructorParameter)
                    .AddParameter(mapperParameter);

                _ = @class.AddImport(new CSharpUsing("Devantler.DataMesh.DataProduct.DataStore.Relational.Entities"))
                    .AddImport(new CSharpUsing("Devantler.DataMesh.DataProduct.DataStore.Relational.Repositories"));
            }

            _ = @class.AddConstructor(constructor);
            _ = codeCompilation.AddType(@class);
        }

        var generator = new CSharpCodeGenerator();
        foreach (var codeItem in generator.Generate(codeCompilation))
        {
            string sourceText = codeItem.Value.AddMetadata(GetType());
            context.AddSource(codeItem.Key, SourceText.From(sourceText, Encoding.UTF8));
        }
    }
}
