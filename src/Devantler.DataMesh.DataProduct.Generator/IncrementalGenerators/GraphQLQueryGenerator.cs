using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.Core;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.CSharp.Model;
using Devantler.Commons.CodeGen.Mapping.Avro;
using Devantler.Commons.StringHelpers;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.FeatureFlags;
using Devantler.DataMesh.DataProduct.Generator.Extensions;
using Devantler.DataMesh.DataProduct.Generator.Models;
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
        if (!options.FeatureFlags.EnableApis.Contains(ApiFeatureFlagValues.GraphQL))
            return new Dictionary<string, string>();

        var schemaRegistryClient = options.SchemaRegistry.CreateSchemaRegistryClient();
        var rootSchema = schemaRegistryClient.GetSchema(options.SchemaRegistry.Schema.Subject, options.SchemaRegistry.Schema.Version);

        var codeCompilation = new CSharpCompilation();

        var avroSchemaParser = new AvroSchemaParser();

        var @class = new CSharpClass("Query")
            .SetIsPartial(true)
            .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IDataStoreService")))
            .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "ISchema")))
            .SetNamespace(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "GraphQLStartupExtensions"));

        foreach (var schema in rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>())
        {
            string schemaName = schema.Name.ToPascalCase();

            var schemaType = schema.Fields.FirstOrDefault(f => f.Name.Equals("id", StringComparison.OrdinalIgnoreCase))?.Type;
            string schemaIdType = schemaType is not null
                ? avroSchemaParser.Parse(schemaType, Language.CSharp)
                : "Guid";

            var method = new CSharpMethod($"Get{schemaName.ToPlural()}")
                .AddAttribute("UsePaging")
                .AddAttribute("UseProjection")
                .AddAttribute("UseFiltering")
                .AddAttribute("UseSorting")
                .SetDocBlock(new CSharpDocBlock($"Queries {schemaName.ToPlural()} from the data store."))
                .SetIsAsynchronous(true)
                .SetReturnType($"Task<IEnumerable<{schemaName}>>")
                .AddParameter(new CSharpParameter($"[Service] IDataStoreService<{schemaIdType}, {schemaName}>", "dataStoreService"))
                .AddParameter(new CSharpParameter("CancellationToken", "cancellationToken"))
                .AddStatement("await dataStoreService.ReadAllAsync(cancellationToken);")
                .SetIsExpressionBodied(true);

            _ = @class.AddMethod(method);
        }

        _ = codeCompilation.AddType(@class);

        var codeGenerator = new CSharpCodeGenerator();
        return codeGenerator.Generate(codeCompilation);
    }
}
