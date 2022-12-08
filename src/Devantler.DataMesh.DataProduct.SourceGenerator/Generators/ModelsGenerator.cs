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
        var schemaRegistryUrl = configuration.GetSection("Kafka").GetSection("SchemaRegistryUrl").Value;
        using var schemaRegistryClient = new CachedSchemaRegistryClient(new SchemaRegistryConfig { Url = schemaRegistryUrl });

        var schema = configuration.GetSection("Schema").Get<Configuration.Schema>();
        var registeredSchemas = new List<RegisteredSchema>
        {
            await schemaRegistryClient.GetRegisteredSchemaAsync(schema.Subject, schema.Version)
        };
        var schemaReferences = registeredSchemas[0].References.ConvertAll(x => schemaRegistryClient.GetRegisteredSchemaAsync(x.Subject, x.Version).Result);
        if (schemaReferences != null)
            registeredSchemas.AddRange(schemaReferences);

        foreach (var registeredSchema in registeredSchemas)
        {
            var @namespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "IModel");
            var schemaRecord = Avro.Schema.Parse(registeredSchema.SchemaString) as RecordSchema;

            var @class = schemaRecord.Name.ToPascalCase();
            var source =
            $$"""
            namespace {{@namespace}};

            public class {{@class}} : IModel
            {
                public Guid Id { get; set; }
                {{AvroFieldParser.Parse(schemaRecord.Fields).IndentBy(4)}}    
            }

            """;

            context.AddSource($"{@class}.cs", SourceText.From(source.AddMetadata(), Encoding.UTF8));
        }
    }
}
