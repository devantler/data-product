using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.CSharp.Model;
using Devantler.Commons.CodeGen.Mapping.Avro.Extensions;
using Devantler.Commons.StringHelpers;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.DataStoreOptions;
using Devantler.DataMesh.DataProduct.Configuration.Options.DataStoreOptions.Relational;
using Devantler.DataMesh.DataProduct.Generator.Models;
using Devantler.DataMesh.SchemaRegistry;
using Microsoft.CodeAnalysis;

namespace Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

/// <summary>
/// A generator to create repositories to interact with data stores.
/// </summary>
[Generator]
public class RepositoryGenerator : GeneratorBase
{
    /// <summary>
    /// Generates repositories.
    /// </summary>
    /// <param name="compilation"></param>
    /// <param name="additionalFiles"></param>
    /// <param name="options"></param>
    public override Dictionary<string, string> Generate(
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

            var baseClass = options.DataStoreOptions.Type switch
            {
                DataStoreType.Relational => new CSharpClass($"EntityFrameworkRepository<{schemaName}Entity>"),
                DataStoreType.DocumentBased => throw new NotSupportedException("Document based data stores are not supported yet."),
                DataStoreType.GraphBased => throw new NotSupportedException("Graph based data stores are not supported yet."),
                _ => throw new NotSupportedException($"The data store type {options.DataStoreOptions.Type} is not supported.")
            };

            var repositoryClass = new CSharpClass($"{schemaName}Repository")
                .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IEntity")))
                .SetNamespace(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IRepository"))
                .SetDocBlock(new CSharpDocBlock($$"""A repository to interact with entities of type <see cref="{{schemaName}}Entity"/>"""))
                .SetBaseClass(baseClass);

            var constructor = options.DataStoreOptions.Type switch
            {
                DataStoreType.Relational
                    => new CSharpConstructor(repositoryClass.Name)
                        .AddParameter(
                            new CSharpConstructorParameter(
                                $"{(options.DataStoreOptions as RelationalDataStoreOptionsBase
                            )?.Provider}DbContext", "context")
                        .SetIsBaseParameter(true)),
                DataStoreType.DocumentBased => throw new NotSupportedException("Document based data stores are not supported yet."),
                DataStoreType.GraphBased => throw new NotSupportedException("Graph based data stores are not supported yet."),
                _ => throw new NotSupportedException($"The data store type {options.DataStoreOptions.Type} is not supported.")
            };

            _ = constructor.SetDocBlock(new CSharpDocBlock($$"""Creates a new instance of the <see cref="{{repositoryClass.Name}}"/> class."""));

            _ = repositoryClass.AddConstructor(constructor);
            _ = codeCompilation.AddType(repositoryClass);
        }

        var codeGenerator = new CSharpCodeGenerator();
        return codeGenerator.Generate(codeCompilation);
    }
}
