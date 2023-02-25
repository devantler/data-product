using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.CSharp.Model;
using Devantler.Commons.CodeGen.Mapping.Avro.Extensions;
using Devantler.Commons.StringHelpers;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Generator.Models;
using Devantler.DataMesh.SchemaRegistry;
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
        var schemaRegistryService = options.GetSchemaRegistryService();
        var rootSchema = schemaRegistryService.GetSchema(options.Schema.Subject, options.Schema.Version);

        var codeCompilation = new CSharpCompilation();

        foreach (var schema in rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>())
        {
            string schemaName = schema.Name.ToPascalCase();

            var baseClass = new CSharpClass($"DataStoreService<{schemaName}, {schemaName}Entity>");

            var @class = new CSharpClass($"{schemaName}DataStoreService")
                .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IModel")))
                .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IEntity")))
                .AddImport(
                    new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IRepository")))
                .AddImport(new CSharpUsing("AutoMapper"))
                .SetNamespace(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IDataStoreService"))
                .SetBaseClass(baseClass);

            var constructor = new CSharpConstructor(@class.Name)
                .AddParameter(new CSharpConstructorParameter($"IRepository<{schemaName}Entity>", "repository")
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
