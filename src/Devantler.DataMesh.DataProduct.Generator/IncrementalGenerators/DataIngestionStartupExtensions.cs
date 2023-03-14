using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.Core.Model;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.CSharp.Model;
using Devantler.Commons.CodeGen.Mapping.Avro;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataIngestors;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.SchemaRegistry;
using Devantler.DataMesh.DataProduct.Generator.Models;
using Devantler.DataMesh.SchemaRegistry;
using Microsoft.CodeAnalysis;

namespace Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

/// <summary>
/// A generator for service registrations and usages for a data ingestion.
/// </summary>
[Generator]
public class DataIngestionStartupExtensions : GeneratorBase
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
        var schemaRegistryService = options.Services.SchemaRegistry.CreateSchemaRegistryService();
        var rootSchema = schemaRegistryService.GetSchema(options.Services.SchemaRegistry.Schema.Subject,
            options.Services.SchemaRegistry.Schema.Version);

        var codeCompilation = new CSharpCompilation();

        var @class = new CSharpClass("DataIngestionStartupExtensions")
            .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace,
                "IDataIngestorOptions")))
            .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IDataIngestor")))
            .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "ISchema")))
            .SetDocBlock(new CSharpDocBlock(
                "A class that contains extension methods for service registrations and usages for data ingestors"))
            .SetNamespace(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "DataIngestionStartupExtensions"))
            .SetIsStatic(true)
            .SetIsPartial(true);

        var servicesParameter = new CSharpParameter("IServiceCollection", "services");
        var optionsParameter = new CSharpParameter("List<IDataIngestorOptions>", "options");
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

        foreach (var schema in schemas)
        {
            foreach (var dataIngestorOptions in options.Services.DataIngestors.GroupBy(x => x.Type).Select(x => x.First()))
            {
                if (dataIngestorOptions.Type == DataIngestorType.Kafka && options.Services.SchemaRegistry.Type != SchemaRegistryType.Kafka)
                    continue;

                _ = addGeneratedServiceRegistrationsMethod.AddStatement(
                    $"_ = services.AddHostedService<{dataIngestorOptions.Type}DataIngestor<{schema.Name}>>();"
                );
            }
        }

        _ = @class.AddMethod(addGeneratedServiceRegistrationsMethod);
        _ = codeCompilation.AddType(@class);

        var codeGenerator = new CSharpCodeGenerator();
        return codeGenerator.Generate(codeCompilation);
    }
}
