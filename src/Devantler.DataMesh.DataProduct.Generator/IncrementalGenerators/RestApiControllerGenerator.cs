using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.Core;
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
/// A generator that generates REST API controllers in the data product.
/// </summary>
[Generator]
public class RestApiControllerGenerator : GeneratorBase
{
    /// <summary>
    /// Generates REST API controllers in the data product.
    /// </summary>
    /// <param name="compilation"></param>
    /// <param name="additionalFiles"></param>
    /// <param name="options"></param>
    public override Dictionary<string, string> Generate(
        Compilation compilation,
        ImmutableArray<AdditionalFile> additionalFiles,
        DataProductOptions options)
    {
        var schemaRegistryService = options.Services.SchemaRegistry.CreateSchemaRegistryService();
        var rootSchema = schemaRegistryService.GetSchema(options.Services.SchemaRegistry.Schema.Subject, options.Services.SchemaRegistry.Schema.Version);

        var codeCompilation = new CSharpCompilation();
        var avroSchemaParser = new AvroSchemaParser();
        foreach (var schema in rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>())
        {
            string schemaName = schema.Name.ToPascalCase();
            var schemaType = schema.Fields.FirstOrDefault(f => f.Name.Equals("id", StringComparison.OrdinalIgnoreCase))?.Type;
            string schemaIdType = schemaType is not null
                ? avroSchemaParser.Parse(schemaType, Language.CSharp)
                : "Guid";
            var @class = new CSharpClass($"{schemaName.ToPlural()}Controller")
                .AddImport(new CSharpUsing("AutoMapper"))
                .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "ISchema")))
                .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IEntity")))
                .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "DataStoreService")))
                .SetNamespace(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "RestApiController"))
                .SetDocBlock(new CSharpDocBlock(
                    $$"""A controller to handle REST API requests for a the <see cref="{{schemaName}}" /> schema."""))
                .SetBaseClass(new CSharpClass($"RestApiController<{schemaIdType}, {schemaName}>"));

            var constructor = new CSharpConstructor(@class.Name)
                .SetDocBlock(new CSharpDocBlock($$"""Creates a new instance of <see cref="{{@class.Name}}" />"""));

            var repositoryConstructorParameter =
                new CSharpConstructorParameter($"IDataStoreService<{schemaIdType}, {schemaName}>", "dataStoreService")
                    .SetIsBaseParameter(true);

            _ = constructor.AddParameter(repositoryConstructorParameter);

            _ = @class.AddConstructor(constructor);
            _ = codeCompilation.AddType(@class);
        }

        var generator = new CSharpCodeGenerator();
        return generator.Generate(codeCompilation);
    }
}
