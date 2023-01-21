using System.Text;
using Devantler.Commons.CodeGen.Core;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.CSharp.Models;
using Devantler.Commons.CodeGen.Mapping.Avro;
using Devantler.DataMesh.DataProduct.Configuration;
using Devantler.DataMesh.DataProduct.Configuration.SchemaRegistry;
using Devantler.DataMesh.SchemaRegistry;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.Extensions.Configuration;

namespace Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

/// <summary>
/// A generator that generates Model classes in the data product.
/// </summary>
[Generator]
public class ModelsGenerator : GeneratorBase
{
    /// <inheritdoc/>
    public override void Generate(SourceProductionContext context, Compilation compilation, IConfiguration configuration, string localSchemaRegistryPath)
    {
        var schemaRegistryOptions = GetSchemaRegistryOptions(configuration, localSchemaRegistryPath);
        var schemaOptions = GetSchemaOptions(configuration);

        var schemaRegistryService = GetSchemaRegistryService(schemaRegistryOptions);

        var rootSchema = schemaRegistryService.GetSchemaAsync(schemaOptions.Subject, schemaOptions.Version).Result;

        AvroCompilationMapper mapper = new();
        var codeCompilation = (CSharpCompilation)mapper.Map(rootSchema, Language.CSharp);
        var generator = new CSharpCodeGenerator();
        foreach (var codeItem in generator.Generate(codeCompilation))
        {
            string sourceText = codeItem.Value.AddMetadata();
            context.AddSource(codeItem.Key, SourceText.From(sourceText, Encoding.UTF8));
        }
    }

    static ISchemaRegistryOptions GetSchemaRegistryOptions(IConfiguration configuration, string localSchemaRegistryPath)
    {
        var schemaRegistryType = configuration
            .GetSection(SchemaRegistryOptionsBase.Key)
            .GetValue<SchemaRegistryType>(nameof(SchemaRegistryOptionsBase.Type));

        switch (schemaRegistryType)
        {
            case SchemaRegistryType.Local:
                var localSchemaRegistryOptions = configuration
                    .GetSection(SchemaRegistryOptionsBase.Key)
                    .Get<LocalSchemaRegistryOptions>()
                    ?? throw new NullReferenceException($"{nameof(LocalSchemaRegistryOptions)} is null");
                localSchemaRegistryOptions.Path = localSchemaRegistryPath;
                return localSchemaRegistryOptions;
            case SchemaRegistryType.Kafka:
                return configuration.GetSection(SchemaRegistryOptionsBase.Key).Get<KafkaSchemaRegistryOptions>()
                    ?? throw new NullReferenceException($"{nameof(KafkaSchemaRegistryOptions)} is null");
            default:
                throw new NotImplementedException($"The specified schema registry type {schemaRegistryType} is not implemented");
        }
    }

    static SchemaOptions GetSchemaOptions(IConfiguration configuration)
    {
        return configuration.GetSection(SchemaOptions.Key).Get<SchemaOptions>() ??
            throw new NullReferenceException($"{nameof(SchemaOptions)} is null");
    }

    ISchemaRegistryService GetSchemaRegistryService(ISchemaRegistryOptions schemaRegistryOptions)
    {
        return schemaRegistryOptions switch
        {
            LocalSchemaRegistryOptions localSchemaRegistryOptions => new LocalSchemaRegistryService(localSchemaRegistryOptions),
            KafkaSchemaRegistryOptions kafkaSchemaRegistryOptions => new KafkaSchemaRegistryService(kafkaSchemaRegistryOptions),
            _ => throw new NotImplementedException($"The specified schema registry type {schemaRegistryOptions.Type} is not implemented"),
        };
    }
}
