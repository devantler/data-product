using System.Collections.Immutable;
using Chr.Avro.Abstract;
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
/// A generator that generates a GraphQL query in the data product.
/// </summary>
[Generator]
public class GraphQLQueryGenerator : GeneratorBase
{
    /// <summary>
    /// Generates a GraphQL query.
    /// </summary>
    /// <param name="compilation"></param>
    /// <param name="additionalFiles"></param>
    /// <param name="options"></param>
    public override Dictionary<string, string> Generate(Compilation compilation,
        ImmutableArray<AdditionalFile> additionalFiles, DataProductOptions options)
    {
        var schemaRegistry = options.GetSchemaRegistryService();
        var rootSchema = schemaRegistry.GetSchema(options.Schema.Subject, options.Schema.Version);

        var codeCompilation = new CSharpCompilation();

        var @class = new CSharpClass("Query")
            .SetIsPartial(true)
            .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IDataStoreService")))
            .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IModel")))
            .SetNamespace(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "GraphQlStartupExtensions"));

        foreach (var schema in rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>())
        {
            string schemaName = schema.Name.ToPascalCase();
            var method = new CSharpMethod($"Get{schemaName.ToPlural()}")
                .AddAttribute("UsePaging")
                .AddAttribute("UseProjection")
                .AddAttribute("UseFiltering")
                .AddAttribute("UseSorting")
                .SetDocBlock(new CSharpDocBlock($"Queries {schemaName.ToPlural()} from the data store."))
                .SetIsAsynchronous(true)
                .SetReturnType($"Task<IQueryable<{schemaName}>>")
                .AddParameter(new CSharpParameter($"[Service] IDataStoreService<{schemaName}>", "dataStoreService"))
                .AddParameter(new CSharpParameter("CancellationToken", "cancellationToken"))
                .AddStatement("await dataStoreService.GetAllAsQueryableAsync(cancellationToken);")
                .SetIsExpressionBodied(true);

            _ = @class.AddMethod(method);
        }

        _ = codeCompilation.AddType(@class);

        var codeGenerator = new CSharpCodeGenerator();
        return codeGenerator.Generate(codeCompilation);
    }
}
