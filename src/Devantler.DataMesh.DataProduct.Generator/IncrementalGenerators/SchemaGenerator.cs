using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.Core;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.CSharp.Model;
using Devantler.Commons.CodeGen.Mapping.Avro;
using Devantler.Commons.StringHelpers;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Generator.Models;
using Devantler.DataMesh.SchemaRegistry;
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
        var schemaRegistryService = options.Services.SchemaRegistry.CreateSchemaRegistryService();
        var rootSchema = schemaRegistryService.GetSchema(options.Services.SchemaRegistry.Schema.Subject,
            options.Services.SchemaRegistry.Schema.Version);

        var codeCompilation = new CSharpCompilation();

        var avroSchemaParser = new AvroSchemaParser();

        foreach (var schema in rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>())
        {
            string schemaName = schema.Name.ToPascalCase();
            var @class = new CSharpClass(schemaName)
                .SetDocBlock(new CSharpDocBlock($"An schema class for the {schemaName} record."))
                .SetNamespace(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "ISchema"))
                .AddImplementation(new CSharpInterface("ISchema"));

            foreach (var field in schema.Fields)
            {
                if (field.Name == "Id")
                    continue;

                string propertyName = field.Name.ToPascalCase();
                string propertyType = avroSchemaParser.Parse(field.Type, Language.CSharp);

                var property = new CSharpProperty(propertyType, propertyName);

                _ = field.Documentation is not null
                    ? property.SetDocBlock(new CSharpDocBlock(field.Documentation))
                    : property.SetDocBlock(new CSharpDocBlock($"The {propertyName} property."));

                _ = @class.AddProperty(property);
            }

            if (!@class.Properties.Any(p => p.Name == "Id"))
            {
                var idProperty = new CSharpProperty("string", "Id")
                    .SetDocBlock(new CSharpDocBlock("The unique identifier for this schema."));
                _ = @class.AddProperty(idProperty);
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
