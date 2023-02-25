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

public class GraphQLQueryGenerator : GeneratorBase
{
    public override Dictionary<string, string> Generate(Compilation compilation,
        ImmutableArray<AdditionalFile> additionalFiles, DataProductOptions options)
    {
        var schemaRegistry = options.GetSchemaRegistryService();
        var rootSchema = schemaRegistry.GetSchema(options.Schema.Subject, options.Schema.Version);

        var codeCompilation = new CSharpCompilation();

        var @class = new CSharpClass("Query")
            .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IDataStoreService")))
            .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IModel")))
            .SetNamespace(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "GraphQlStartupExtensions"));

        foreach (var schema in rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>())
        {
            var schemaName = schema.Name.ToPascalCase();
            var method = new CSharpMethod($"Get{schemaName.ToPlural()}")
                .AddAttribute("UseProjection")
                .AddAttribute("UseFiltering")
                .AddAttribute("UseSorting")
                .SetIsAsync(true)
                .SetReturnType($"Task<IEnumerable<{schemaName}>>")
                .AddParameter(new CSharpParameter($"[Service] IDataStoreService<{schemaName}>", "dataStoreService"))
                .AddParameter(new CSharpParameter("CancellationToken", "cancellationToken"))
                .AddStatement("await dataStoreService.GetAllAsync(cancellationToken);")
                .SetIsExpressionBodiedMethod(true);
        }





        var codeGenerator = new CSharpCodeGenerator();
        codeGenerator.Generate(codeCompilation);
    }
}
