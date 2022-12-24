using System.Collections.Generic;
using System.Text;
using Avro;
using Confluent.SchemaRegistry;
using Devantler.DataMesh.DataProduct.SourceGenerator.Core;
using Devantler.DataMesh.DataProduct.SourceGenerator.Core.Extensions;
using Devantler.DataMesh.DataProduct.SourceGenerator.Core.Parsers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.Extensions.Configuration;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Generators;

[Generator]
public class ModelsGenerator : AppSettingsGenerator
{
    protected override async void Generate(SourceProductionContext context, Compilation compilation, IConfiguration configuration)
    {
        var schemaRegistryOptions = configuration.GetSection(nameof(Configuration.SchemaRegistry)).Get<Configuration.SchemaRegistry>();
        var schemaOptions = configuration.GetSection(nameof(Configuration.Schema)).Get<Configuration.Schema>();

        var schemas = new List<RecordSchema>();
        switch (schemaRegistryOptions.Type)
        {
            case Configuration.SchemaRegistryType.Local:
                var schemaFile = System.IO.Directory.GetFiles(schemaRegistryOptions.Path, $"{schemaOptions.Subject.ToCamelCase()}-v{schemaOptions.Version}.avsc");
                if (schemaFile.Length == 0)
                    throw new System.IO.FileNotFoundException($"Schema file not found for {schemaOptions.Subject}-{schemaOptions.Version}.avsc");

                var schemaString = System.IO.File.ReadAllText(schemaFile[0]);
                schemas.Add(Avro.Schema.Parse(schemaString) as RecordSchema);

                break;
            case Configuration.SchemaRegistryType.Kafka:
                var cachedSchemaRegistryClient = new CachedSchemaRegistryClient(new SchemaRegistryConfig { Url = schemaRegistryOptions.Url });
                var registeredSchemas = new List<RegisteredSchema>
                {
                     await cachedSchemaRegistryClient.GetRegisteredSchemaAsync(schemaOptions.Subject, schemaOptions.Version)
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
                throw new System.NotImplementedException($"Schema registry type {schemaRegistryOptions.Type} not implemented");
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

            context.AddSource($"{@class}.cs", SourceText.From(source.AddMetadata(), Encoding.UTF8));
        }
    }
}
