using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.Core;
using Devantler.Commons.CodeGen.Core.Model;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.CSharp.Model;
using Devantler.Commons.CodeGen.Mapping.Avro;
using Devantler.Commons.StringHelpers;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.DataIngestors;
using Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistry;
using Devantler.DataMesh.DataProduct.Generator.Extensions;
using Devantler.DataMesh.DataProduct.Generator.Models;
using Microsoft.CodeAnalysis;

namespace Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

/// <summary>
/// A generator for service registrations and usages for a data ingestion.
/// </summary>
[Generator]
public class DataIngestionStartupExtensionsGenerator : GeneratorBase
{
    /// <summary>
    /// Generates service registrations and usages for a data store.
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
        var rootSchema = schemaRegistryClient.GetSchema(options.SchemaRegistry.Schema.Subject,
            options.SchemaRegistry.Schema.Version);

        var codeCompilation = new CSharpCompilation();

        var @class = new CSharpClass("DataIngestionStartupExtensions")
            .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "DataIngestionStartupExtensions") + ".Services"))
            .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "DataIngestorOptions").NullIfEmpty()
                ?? "Devantler.DataMesh.DataProduct.Configuration.Options.DataIngestors")
            )
            .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "ISchema")))
            .SetDocBlock(new CSharpDocBlock(
                "A class that contains extension methods for service registrations and usages for data ingestors"))
            .SetNamespace(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "DataIngestionStartupExtensions"))
            .SetIsStatic(true)
            .SetIsPartial(true);

        var servicesParameter = new CSharpParameter("IServiceCollection", "services");
        var optionsParameter = new CSharpParameter("List<DataIngestorOptions>", "options");
        var addGeneratedServiceRegistrationsMethod = new CSharpMethod("AddGeneratedServiceRegistrations")
            .SetIsStatic(true)
            .SetIsPartial(true)
            .SetIsExtensionMethod(true)
            .SetDocBlock(new CSharpDocBlock("Adds generated service registrations for data ingestors."))
            .SetVisibility(Visibility.Private)
            .AddParameter(servicesParameter)
            .AddParameter(optionsParameter);

        var schemas = rootSchema is RecordSchema recordSchema
            ? new[] { recordSchema }
            : rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>();

        var avroSchemaParser = new AvroSchemaParser();

        foreach (var schema in schemas)
        {
            var schemaType = schema.Fields.FirstOrDefault(f => f.Name.Equals("id", StringComparison.OrdinalIgnoreCase))?.Type;
            string schemaIdType = schemaType is not null
                ? avroSchemaParser.Parse(schemaType, Language.CSharp)
                : "Guid";
            foreach (var dataIngestorOptions in options.DataIngestors.GroupBy(x => x.Type).Select(x => x.First()))
            {
                if (dataIngestorOptions.Type == DataIngestorType.Kafka && options.SchemaRegistry.Type != SchemaRegistryType.Kafka)
                    continue;

                _ = addGeneratedServiceRegistrationsMethod.AddStatement(
                    $"_ = services.AddHostedService<{dataIngestorOptions.Type}DataIngestorService<{schemaIdType}, {schema.Name}>>();"
                );
            }
        }

        _ = @class.AddMethod(addGeneratedServiceRegistrationsMethod);
        _ = codeCompilation.AddType(@class);

        var codeGenerator = new CSharpCodeGenerator();
        return codeGenerator.Generate(codeCompilation);
    }
}
