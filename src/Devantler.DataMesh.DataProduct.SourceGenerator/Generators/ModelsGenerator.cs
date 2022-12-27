using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avro;
using Confluent.SchemaRegistry;
using Devantler.DataMesh.DataProduct.Configuration;
using Devantler.DataMesh.DataProduct.SourceGenerator.Extensions;
using Devantler.DataMesh.DataProduct.SourceGenerator.Parsers;
using Devantler.DataMesh.DataProduct.SourceGenerator.Resolvers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Generators;

[Generator]
public class ModelsGenerator : GeneratorBase
{
    protected override async void Generate(SourceProductionContext context, Compilation compilation, DataProductOptions options)
    {
        var schemas = new List<RecordSchema>();
        switch (options.SchemaRegistry.Type)
        {
            case SchemaRegistryType.Local:
                var schemaFile = System.IO.Directory.GetFiles(options.SchemaRegistry.Path, $"{options.Schema.Subject.ToCamelCase()}-v{options.Schema.Version}.avsc");
                if (schemaFile.Length == 0)
                    throw new System.IO.FileNotFoundException($"Schema file not found for {options.Schema.Subject.ToCamelCase()}-{options.Schema.Version}.avsc");

                var schemaString = System.IO.File.ReadAllText(schemaFile[0]);

                switch (Avro.Schema.Parse(schemaString))
                {
                    case RecordSchema recordSchema:
                        schemas.Add(recordSchema);
                        break;
                    case UnionSchema unionSchema:
                        schemas.AddRange(unionSchema.Schemas.Where(x => x is RecordSchema).Select(x => x as RecordSchema));
                        break;
                }
                break;
            case SchemaRegistryType.Kafka:
                var cachedSchemaRegistryClient = new CachedSchemaRegistryClient(new SchemaRegistryConfig { Url = options.SchemaRegistry.Url });
                var registeredSchemas = new List<RegisteredSchema>
                {
                     await cachedSchemaRegistryClient.GetRegisteredSchemaAsync(options.Schema.Subject, options.Schema.Version)
                };
                var schemaReferences = registeredSchemas[0].References.ConvertAll(x => cachedSchemaRegistryClient.GetRegisteredSchemaAsync(x.Subject, x.Version).Result);
                if (schemaReferences != null)
                    registeredSchemas.AddRange(schemaReferences);

                foreach (var registeredSchema in registeredSchemas)
                {
                    schemas.Add(Avro.Schema.Parse(registeredSchema.SchemaString) as RecordSchema);
                }
                break;
            default:
                throw new System.NotImplementedException($"Schema registry type {options.SchemaRegistry.Type} not implemented");
        }

        foreach (var schema in schemas)
        {
            var @namespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "IModel");

            var @class = schema.Name.ToPascalCase();
            var source =
            $$"""
            namespace {{@namespace}};

            public class {{@class}} : IModel
            {
                public Guid Id { get; set; }
                {{AvroFieldParser.Parse(schema.Fields).IndentBy(4)}}    
            }

            """;

            context.AddSource($"{@class}.cs", SourceText.From(source, Encoding.UTF8));
        }
    }
}
