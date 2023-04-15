using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.Core;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.CSharp.Model;
using Devantler.Commons.CodeGen.Mapping.Avro;
using Devantler.Commons.StringHelpers.Extensions;
using Devantler.DataProduct.Core.Configuration.Options;
using Devantler.DataProduct.Core.Configuration.Options.DataStore;
using Devantler.DataProduct.Generator.Extensions;
using Devantler.DataProduct.Generator.Models;
using Microsoft.CodeAnalysis;

namespace Devantler.DataProduct.Generator.IncrementalGenerators;

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
        var schemaRegistryClient = options.SchemaRegistry.CreateSchemaRegistryClient();
        var rootSchema = schemaRegistryClient.GetSchema(options.SchemaRegistry.Schema.Subject, options.SchemaRegistry.Schema.Version);

        var codeCompilation = new CSharpCompilation();
        var avroSchemaParser = new AvroSchemaParser();
        foreach (var schema in rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>())
        {
            string schemaName = schema.Name.ToPascalCase();
            var schemaType = schema.Fields.FirstOrDefault(f => f.Name.Equals("id", StringComparison.OrdinalIgnoreCase))?.Type;
            string schemaIdType = schemaType is not null
                ? avroSchemaParser.Parse(schemaType, Language.CSharp)
                : "Guid";

            var baseClass = options.DataStore.Type switch
            {
                DataStoreType.SQL => new CSharpClass($"SQLRepository<{schemaIdType}, {schemaName}Entity>")
                    .SetDocBlock(new CSharpDocBlock($$"""A repository to interact with entities of type <see cref="{{schemaName}}Entity"/>""")),
                DataStoreType.NoSQL => throw new NotSupportedException("Document based data stores are not supported yet."),
                DataStoreType.Graph => throw new NotSupportedException("Graph based data stores are not supported yet."),
                _ => throw new NotSupportedException($"The data store type {options.DataStore.Type} is not supported.")
            };

            var repositoryClass = new CSharpClass($"{schemaName}Repository")
                .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IEntity")))
                .AddImport(new CSharpUsing("Microsoft.EntityFrameworkCore"))
                .SetNamespace(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IRepository"))
                .SetDocBlock(new CSharpDocBlock($$"""A repository to interact with entities of type <see cref="{{schemaName}}Entity"/>"""))
                .SetBaseClass(baseClass);

            var constructor = options.DataStore.Type switch
            {
                DataStoreType.SQL
                    => new CSharpConstructor(repositoryClass.Name)
                        .SetDocBlock(new CSharpDocBlock($$"""Creates a new instance of the <see cref="{{repositoryClass.Name}}"/> class."""))
                        .AddParameter(
                            new CSharpConstructorParameter(
                                $"IDbContextFactory<{options.DataStore.Provider}DbContext>", "dbContextFactory")
                        .SetIsBaseParameter(true, "dbContextFactory.CreateDbContext()")),
                DataStoreType.NoSQL => throw new NotSupportedException("Document based data stores are not supported yet."),
                DataStoreType.Graph => throw new NotSupportedException("Graph based data stores are not supported yet."),
                _ => throw new NotSupportedException($"The data store type {options.DataStore.Type} is not supported.")
            };

            _ = constructor.SetDocBlock(new CSharpDocBlock($$"""Creates a new instance of the <see cref="{{repositoryClass.Name}}"/> class."""));

            _ = repositoryClass.AddConstructor(constructor);
            _ = codeCompilation.AddType(repositoryClass);
        }

        var codeGenerator = new CSharpCodeGenerator();
        return codeGenerator.Generate(codeCompilation);
    }
}
