using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.Core;
using Devantler.Commons.CodeGen.Core.Model;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.CSharp.Model;
using Devantler.Commons.CodeGen.Mapping.Avro;
using Devantler.Commons.StringHelpers.Extensions;
using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.Inputs;
using Devantler.DataProduct.Configuration.Options.SchemaRegistry;
using Devantler.DataProduct.Generator.Extensions;
using Devantler.DataProduct.Generator.Models;
using Microsoft.CodeAnalysis;

namespace Devantler.DataProduct.Generator.IncrementalGenerators;

/// <summary>
/// A generator for service registrations and usages for inputs.
/// </summary>
[Generator]
public class InputsStartupExtensionsGenerator : GeneratorBase
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
        if (!options.FeatureFlags.EnableInputs || !options.Inputs.Any())
            return new Dictionary<string, string>();

        var schemaRegistryClient = options.SchemaRegistry.CreateSchemaRegistryClient();
        var rootSchema = schemaRegistryClient.GetSchema(options.SchemaRegistry.Schema.Subject,
            options.SchemaRegistry.Schema.Version);

        var codeCompilation = new CSharpCompilation();

        var @class = new CSharpClass("InputsStartupExtensions")
            .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "InputsStartupExtensions") + ".Services"))
            .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "InputOptions").NullIfEmpty()
                ?? "Devantler.DataProduct.Configuration.Options.Inputs")
            )
            .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "ISchema")))
            .SetDocBlock(new CSharpDocBlock(
                "A class that contains extension methods for service registrations and usages for inputs"))
            .SetNamespace(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "InputsStartupExtensions"))
            .SetIsStatic(true)
            .SetIsPartial(true);

        var servicesParameter = new CSharpParameter("IServiceCollection", "services");
        var optionsParameter = new CSharpParameter("List<InputOptions>", "options");
        var addGeneratedServiceRegistrationsMethod = new CSharpMethod("AddGeneratedServiceRegistrations")
            .SetIsStatic(true)
            .SetIsPartial(true)
            .SetIsExtensionMethod(true)
            .SetDocBlock(new CSharpDocBlock("Adds generated service registrations for inputs."))
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
            foreach (var dataIngestorOptions in options.Inputs.GroupBy(x => x.Type).Select(x => x.First()))
            {
                if (dataIngestorOptions.Type == InputType.Kafka && options.SchemaRegistry.Type != SchemaRegistryType.Kafka)
                    continue;

                _ = addGeneratedServiceRegistrationsMethod.AddStatement(
                    $"_ = services.AddHostedService<{dataIngestorOptions.Type}InputService<{schemaIdType}, {schema.Name}>>();"
                );
            }
        }

        _ = @class.AddMethod(addGeneratedServiceRegistrationsMethod);
        _ = codeCompilation.AddType(@class);

        var codeGenerator = new CSharpCodeGenerator();
        return codeGenerator.Generate(codeCompilation);
    }
}
