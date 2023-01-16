using System.Text;
using Avro;
using Devantler.Commons.StringHelpers;
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
    public override void Generate(string assemblyPath, SourceProductionContext context, Compilation compilation, IConfiguration configuration)
    {
        ISchemaRegistryOptions schemaRegistryOptions = GetSchemaRegistryOptions(configuration);
        SchemaOptions schemaOptions = GetSchemaOptions(configuration);

        ISchemaRegistryService schemaRegistryService = GetSchemaRegistryService(schemaRegistryOptions, assemblyPath);

        Schema rootSchema = schemaRegistryService.GetSchemaAsync(schemaOptions.Subject, schemaOptions.Version).Result;

        AvroCodeCollectionMapper<Schema> mapper = new();
        foreach (Schema schema in GetFlattenedSchemas(rootSchema))
        {
            AvroCodeGenerator codeGenerator = new();
            string model = codeGenerator.Generate("Devantler.DataMesh.DataProduct.Models", schema);
        }
        context.AddSource($"{schema.Name.ToPascalCase()}.g.cs", SourceText.From(model, Encoding.UTF8));
    }

    static ISchemaRegistryOptions GetSchemaRegistryOptions(IConfiguration configuration)
    {
        SchemaRegistryType schemaRegistryType = configuration
            .GetSection(SchemaRegistryOptionsBase.Key)
            .GetValue<SchemaRegistryType>(nameof(SchemaRegistryOptionsBase.Type));

        return schemaRegistryType switch
        {
            SchemaRegistryType.Local => configuration.GetSection(SchemaRegistryOptionsBase.Key).Get<LocalSchemaRegistryOptions>() ??
                    throw new NullReferenceException($"{nameof(LocalSchemaRegistryOptions)} is null"),
            SchemaRegistryType.Kafka => configuration.GetSection(SchemaRegistryOptionsBase.Key).Get<KafkaSchemaRegistryOptions>() ??
                    throw new NullReferenceException($"{nameof(KafkaSchemaRegistryOptions)} is null"),
            _ => throw new NotImplementedException($"The specified schema registry type {schemaRegistryType} is not implemented")
        };
    }

    static SchemaOptions GetSchemaOptions(IConfiguration configuration)
    {
        return configuration.GetSection(SchemaOptions.Key).Get<SchemaOptions>() ??
            throw new NullReferenceException($"{nameof(SchemaOptions)} is null");
    }

    ISchemaRegistryService GetSchemaRegistryService(ISchemaRegistryOptions schemaRegistryOptions, string assemblyPath)
    {
        //return a switch statement that casts the schemaRegistryOptions to the correct type and returns the correct service
        return schemaRegistryOptions switch
        {
            LocalSchemaRegistryOptions localSchemaRegistryOptions => new LocalSchemaRegistryService(new LocalSchemaRegistryOptions
            {
                Path = localSchemaRegistryOptions.Path?.StartsWith("/") == true ?
                    localSchemaRegistryOptions.Path :
                    assemblyPath + localSchemaRegistryOptions.Path
            }
            ),
            KafkaSchemaRegistryOptions kafkaSchemaRegistryOptions => new KafkaSchemaRegistryService(new KafkaSchemaRegistryOptions { Url = kafkaSchemaRegistryOptions.Url }),
            _ => throw new NotImplementedException($"Schema registry type {schemaRegistryOptions.Type} not implemented")
        };
    }
}
