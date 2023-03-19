using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.Core;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.CSharp.Model;
using Devantler.Commons.CodeGen.Mapping.Avro;
using Devantler.Commons.StringHelpers;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Generator.Extensions;
using Devantler.DataMesh.DataProduct.Generator.Models;
using Microsoft.CodeAnalysis;

namespace Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

/// <summary>
/// A generator that generates Schema classes in the data product.
/// </summary>
[Generator]
public class SchemaGenerator : GeneratorBase
{
    /// <inheritdoc/>
    public override Dictionary<string, string> Generate(
        Compilation compilation,
        ImmutableArray<AdditionalFile> additionalFiles,
        DataProductOptions options)
    {
        var schemaRegistryService = options.SchemaRegistry.CreateSchemaRegistryService();
        var rootSchema = schemaRegistryService.GetSchema(options.SchemaRegistry.Schema.Subject,
            options.SchemaRegistry.Schema.Version);

        var codeCompilation = new CSharpCompilation();

        var avroSchemaParser = new AvroSchemaParser();

        foreach (var schema in rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>())
        {
            string schemaName = schema.Name.ToPascalCase();
            var @class = new CSharpClass(schemaName)
                .SetDocBlock(new CSharpDocBlock($"An schema class for the {schemaName} record."))
                .SetNamespace(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "ISchema"));

            var idProperty = new CSharpProperty("Guid", "Id")
                .SetDocBlock(new CSharpDocBlock("The unique identifier for this schema."));
            if (schema.Fields.Any(f => f.Name.Equals("id", StringComparison.OrdinalIgnoreCase)))
            {
                var idField = schema.Fields.First(f => f.Name.Equals("id", StringComparison.OrdinalIgnoreCase));
                idProperty.Type = avroSchemaParser.Parse(idField.Type, Language.CSharp);
            };
            _ = @class.AddProperty(idProperty);
            _ = @class.AddImplementation(new CSharpInterface($"ISchema<{idProperty.Type}>"));

            foreach (var field in schema.Fields)
            {
                if (field.Name.Equals("id", StringComparison.OrdinalIgnoreCase))
                    continue;

                string propertyName = field.Name.ToPascalCase();
                string propertyType = avroSchemaParser.Parse(field.Type, Language.CSharp);

                var property = new CSharpProperty(propertyType, propertyName);

                _ = field.Documentation is not null
                    ? property.SetDocBlock(new CSharpDocBlock(field.Documentation))
                    : property.SetDocBlock(new CSharpDocBlock($"The {propertyName} property."));

                _ = @class.AddProperty(property);
            }

            _ = codeCompilation.AddType(@class);
        }

        foreach (var schema in rootSchema.Flatten().FindAll(s => s is EnumSchema).Cast<EnumSchema>())
        {
            string schemaName = schema.Name.ToPascalCase();
            var @enum = new CSharpEnum(schemaName)
                .SetDocBlock(new CSharpDocBlock($"An enum class for the {schemaName} record."))
                .SetNamespace(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "ISchema"));

            foreach (string symbol in schema.Symbols)
            {
                _ = @enum.AddValue(
                    new CSharpEnumSymbol(symbol)
                        .SetDocBlock(new CSharpDocBlock($"The {symbol} value."))
                );
            }

            _ = codeCompilation.AddType(@enum);
        }

        var generator = new CSharpCodeGenerator();
        return generator.Generate(codeCompilation);
    }
}
