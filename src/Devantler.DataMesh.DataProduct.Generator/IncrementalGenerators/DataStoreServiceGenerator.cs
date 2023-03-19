using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.Core;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.CSharp.Model;
using Devantler.Commons.CodeGen.Mapping.Avro;
using Devantler.Commons.StringHelpers;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Generator.Extensions;
using Devantler.DataMesh.DataProduct.Generator.Models;
using Microsoft.CodeAnalysis;

namespace Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

/// <summary>
/// A generator that generates a data store service for a given schema.
/// </summary>
[Generator]
public class DataStoreServiceGenerator : GeneratorBase
{
    /// <summary>
    /// Generates a data store service for a given schema.
    /// </summary>
    /// <param name="compilation"></param>
    /// <param name="additionalFiles"></param>
    /// <param name="options"></param>
    public override Dictionary<string, string> Generate(Compilation compilation,
        ImmutableArray<AdditionalFile> additionalFiles, DataProductOptions options)
    {
        var schemaRegistryService = options.SchemaRegistry.CreateSchemaRegistryService();
        var rootSchema = schemaRegistryService.GetSchema(options.SchemaRegistry.Schema.Subject, options.SchemaRegistry.Schema.Version);

        var codeCompilation = new CSharpCompilation();
        var avroSchemaParser = new AvroSchemaParser();
        foreach (var schema in rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>())
        {
            string schemaName = schema.Name.ToPascalCase();
            var schemaType = schema.Fields.FirstOrDefault(f => f.Name.Equals("id", StringComparison.OrdinalIgnoreCase))?.Type;
            string schemaIdType = schemaType is not null
                ? avroSchemaParser.Parse(schemaType, Language.CSharp)
                : "Guid";

            var baseClass = new CSharpClass($"DataStoreService<{schemaIdType}, {schemaName}, {schemaName}Entity>");

            var @class = new CSharpClass($"{schemaName}DataStoreService")
                .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "ISchema")))
                .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IEntity")))
                .AddImport(
                    new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IRepository")))
                .AddImport(new CSharpUsing("AutoMapper"))
                .SetNamespace(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IDataStoreService"))
                .SetDocBlock(new CSharpDocBlock($"""A data store service for the <see cref="{schemaName}" /> schema."""))
                .SetBaseClass(baseClass);

            var constructor = new CSharpConstructor(@class.Name)
                .SetDocBlock(new CSharpDocBlock($"""Creates a new instance of <see cref="{@class.Name}" />"""))
                .AddParameter(new CSharpConstructorParameter($"IRepository<{schemaIdType}, {schemaName}Entity>", "repository")
                    .SetIsBaseParameter(true))
                .AddParameter(new CSharpConstructorParameter("IServiceProvider", "serviceProvider")
                    .SetIsBaseParameter(true))
                .AddParameter(new CSharpConstructorParameter("IMapper", "mapper")
                    .SetIsBaseParameter(true));

            _ = @class.AddConstructor(constructor);
            _ = codeCompilation.AddType(@class);
        }

        var codeGenerator = new CSharpCodeGenerator();
        return codeGenerator.Generate(codeCompilation);
    }
}
