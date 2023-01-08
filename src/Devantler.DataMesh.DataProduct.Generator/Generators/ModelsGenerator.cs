using System.CodeDom.Compiler;
using System.Text;
using Avro;
using Devantler.Commons.StringHelpers;
using Devantler.DataMesh.DataProduct.Configuration;
using Devantler.DataMesh.SchemaRegistry.Providers;
using Devantler.DataMesh.SchemaRegistry.Providers.Kafka;
using Devantler.DataMesh.SchemaRegistry.Providers.Local;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CSharp;
using Microsoft.Extensions.Configuration;

namespace Devantler.DataMesh.DataProduct.Generator.Generators;

[Generator]
public class ModelsGenerator : GeneratorBase
{
    ISchemaRegistryService _schemaRegistryService = null!;
    readonly CSharpCodeProvider _codeProvider = new();

    public override void Generate(string assemblyPath, SourceProductionContext context, Compilation compilation, IConfiguration configuration)
    {
        SchemaRegistryOptions schemaRegistryOptions = configuration.GetSection("DataProduct:SchemaRegistry").Get<SchemaRegistryOptions>() ??
            throw new NullReferenceException($"{nameof(SchemaRegistryOptions)} is null");

        SchemaOptions schemaOptions = configuration.GetSection("DataProduct:Schema").Get<SchemaOptions>() ??
            throw new NullReferenceException($"{nameof(SchemaOptions)} is null");

        _schemaRegistryService = GetSchemaRegistry(schemaRegistryOptions, assemblyPath);

        Schema rootSchema = _schemaRegistryService.GetSchemaAsync(schemaOptions.Subject, schemaOptions.Version).Result;

        foreach (RecordSchema schema in GetRecordSchemas(rootSchema))
        {
            string modelCodeString = GenerateModel(schema);

            context.AddSource($"{schema.Name.ToPascalCase()}.cs", SourceText.From(modelCodeString, Encoding.UTF8));
        }
    }

    ISchemaRegistryService GetSchemaRegistry(SchemaRegistryOptions? schemaRegistryOptions, string assemblyPath)
    {
        return schemaRegistryOptions?.Type switch
        {
            SchemaRegistryType.Local => new LocalSchemaRegistryService(new LocalSchemaRegistryOptions
            {
                Path = schemaRegistryOptions.Path?.StartsWith("/") == true ?
                    schemaRegistryOptions.Path :
                    assemblyPath + schemaRegistryOptions.Path
            }
            ),
            SchemaRegistryType.Kafka => new KafkaSchemaRegistryService(new KafkaSchemaRegistryOptions { Url = schemaRegistryOptions.Url }),
            _ => throw new NotImplementedException($"Schema registry type {schemaRegistryOptions?.Type} not implemented")
        };
    }

    static RecordSchema[] GetRecordSchemas(Schema rootSchema)
    {
        return rootSchema switch
        {
            RecordSchema recordSchema => new[] { recordSchema },
            UnionSchema unionSchema => unionSchema.Schemas.OfType<RecordSchema>().ToArray(),
            _ => throw new NotImplementedException($"Schema type {rootSchema.GetType()} not implemented")
        };
    }

    string GenerateModel(RecordSchema schema)
    {
        if (!schema.Fields.Any(x => x.Name.Equals("id", StringComparison.OrdinalIgnoreCase)))
            schema.Fields = AddCustomFields(schema.Fields);

        CodeGen codeGen = new();
        codeGen.AddSchema(schema.ToString(), new List<KeyValuePair<string, string>>() {
            new(schema.Namespace, "Devantler.DataMesh.DataProduct.Models")
        });

        CodeGeneratorOptions codeGeneratorOptions = new()
        {
            BlankLinesBetweenMembers = false
        };
        using StringWriter writer = new();
        _codeProvider.GenerateCodeFromCompileUnit(codeGen.GenerateCode(), writer, codeGeneratorOptions);

        return writer.ToString();
    }

    static List<Field> AddCustomFields(List<Field> fields)
    {
        List<Field> customFields = new()
        {
            new Field(PrimitiveSchema.Create(Schema.Type.String), "Id", 0)
        };
        List<Field> updatedFields = fields.ConvertAll(x => new Field(x.Schema, x.Name.ToPascalCase(), x.Pos, x.Aliases, x.Documentation, x.DefaultValue, x.Ordering ?? Field.SortOrder.ignore));
        return customFields.Concat(fields).ToList();
    }
}
