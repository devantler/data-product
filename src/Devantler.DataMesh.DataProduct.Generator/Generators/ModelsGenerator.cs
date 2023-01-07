using System.Text;
using Avro;
using Devantler.Commons.StringHelpers;
using Devantler.DataMesh.DataProduct.Configuration;
using Devantler.DataMesh.DataProduct.Generator.Parsers;
using Devantler.DataMesh.DataProduct.Generator.Resolvers;
using Devantler.DataMesh.SchemaRegistry.Providers;
using Devantler.DataMesh.SchemaRegistry.Providers.Kafka;
using Devantler.DataMesh.SchemaRegistry.Providers.Local;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Devantler.DataMesh.DataProduct.Generator.Generators;

[Generator]
public class ModelsGenerator : GeneratorBase
{
    private ISchemaRegistryService _schemaRegistryService;

    public override void Generate(string assemblyPath, SourceProductionContext context, Compilation compilation, DataProductOptions options)
    {
        Validate(options);

        _schemaRegistryService = Resolve(options.SchemaRegistry, assemblyPath);

        var rootSchema = _schemaRegistryService.GetSchemaAsync(options.Schema.Subject, options.Schema.Version).Result;

        foreach (var schema in Resolve(rootSchema))
        {
            var className = schema.Name.ToPascalCase();

            string modelString = GenerateModel(compilation, schema, className);

            context.AddSource($"{className}.cs", SourceText.From(modelString, Encoding.UTF8));
        }
    }

    private static string GenerateModel(Compilation compilation, RecordSchema schema, string className)
    {
        var @namespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "IModel");

        return $$"""
            namespace {{@namespace}};

            public class {{className}} : IModel
            {
                public Guid Id { get; set; }
                {{AvroFieldParser.Parse(schema.Fields).Indent(4)}}    
            }

            """;
    }

    private static void Validate(DataProductOptions options)
    {
        if (options.SchemaRegistry == null)
            throw new InvalidOperationException($"{nameof(options.SchemaRegistry)} not set");

        if (options.Schema == null)
            throw new InvalidOperationException($"{nameof(options.Schema)} not set");
    }

    private static RecordSchema[] Resolve(Schema rootSchema)
    {
        return rootSchema switch
        {
            RecordSchema recordSchema => new[] { recordSchema },
            UnionSchema unionSchema => unionSchema.Schemas.OfType<RecordSchema>().ToArray(),
            _ => throw new NotImplementedException($"Schema type {rootSchema.GetType()} not implemented")
        };
    }

    private ISchemaRegistryService Resolve(SchemaRegistryOptions schemaRegistryOptions, string assemblyPath)
    {
        return schemaRegistryOptions.Type switch
        {
            SchemaRegistryType.Local => new LocalSchemaRegistryService(new LocalSchemaRegistryOptions
            {
                Path = schemaRegistryOptions.Path.StartsWith("/") ?
                    schemaRegistryOptions.Path :
                    assemblyPath + schemaRegistryOptions.Path
            }
            ),
            SchemaRegistryType.Kafka => new KafkaSchemaRegistryService(new KafkaSchemaRegistryOptions { Url = schemaRegistryOptions.Url }),
            _ => throw new NotImplementedException($"Schema registry type {schemaRegistryOptions.Type} not implemented")
        };
    }
}
