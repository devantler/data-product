using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.CSharp.Model;
using Devantler.Commons.CodeGen.Mapping.Avro.Extensions;
using Devantler.Commons.StringHelpers;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Generator.Models;
using Devantler.DataMesh.SchemaRegistry;
using Microsoft.CodeAnalysis;

namespace Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

[Generator]
public class RepositoryGenerator : GeneratorBase
{
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

            var repositoryClass = new CSharpClass($"{schemaName}Repository")
                .SetNamespace(NamespaceResolver.ResolveForType(compilation.GlobalNamespace,
                    "RelationalDataStoreStartupExtensions"))
                .SetDocBlock(new CSharpDocBlock(
                    $$"""A repository to interact with entities of type <see cref="{{schemaName}}Entity"/>"""))
                .SetBaseClass(new CSharpClass($"EntityFrameworkRepository<{schemaName}Entity>"));

            var constructor = new CSharpConstructor(repositoryClass.Name)
                .AddParameter(new CSharpConstructorParameter("SqliteDbContext", "context")
                    .SetIsBaseParameter(true));

            _ = repositoryClass.AddConstructor(constructor);
            _ = codeCompilation.AddType(repositoryClass);
        }

        return codeCompilation.Compile();
    }
}