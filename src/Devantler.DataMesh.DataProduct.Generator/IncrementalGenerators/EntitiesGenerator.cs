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
/// A generator that generates Entity classes in the data product.
/// </summary>
[Generator]
public class EntitiesGenerator : GeneratorBase
{
    /// <inheritdoc/>
    public override Dictionary<string, string> Generate(
        Compilation compilation,
        ImmutableArray<AdditionalFile> additionalFiles,
        DataProductOptions options)
    {
        var schemaRegistryService = options.Services.SchemaRegistry.CreateSchemaRegistryService();
        var rootSchema = schemaRegistryService.GetSchema(options.Services.SchemaRegistry.Schema.Subject, options.Services.SchemaRegistry.Schema.Version);

        var codeCompilation = new CSharpCompilation();

        var avroSchemaParser = new AvroSchemaParser();

        foreach (var schema in rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>())
        {
            string schemaName = schema.Name.ToPascalCase();
            var @class = new CSharpClass($"{schemaName}Entity")
                .SetDocBlock(new CSharpDocBlock($"An entity class for the {schemaName} record."))
                .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "ISchema")))
                .SetNamespace(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IEntity"))
                .AddImplementation(new CSharpInterface("IEntity"));

            var idProperty = new CSharpProperty("string", "Id")
                .SetDocBlock(new CSharpDocBlock("The unique identifier for this schema."));
            _ = @class.AddProperty(idProperty);

            foreach (var field in schema.Fields)
            {
                if (field.Name.Equals("id", StringComparison.OrdinalIgnoreCase))
                    continue;

                string propertyName = field.Name.ToPascalCase();
                string propertyType = avroSchemaParser.Parse(field.Type, Language.CSharp, action => action.RecordSuffix = "Entity");
                bool isVirtual = field.Type switch
                {
                    RecordSchema => true,
                    ArraySchema => true,
                    MapSchema => true,
                    _ => false
                };
                var property = new CSharpProperty((isVirtual ? "virtual " : string.Empty) + propertyType, propertyName);

                _ = field.Documentation is not null
                    ? property.SetDocBlock(new CSharpDocBlock(field.Documentation))
                    : property.SetDocBlock(new CSharpDocBlock($"The {propertyName} property."));

                _ = @class.AddProperty(property);
            }

            _ = codeCompilation.AddType(@class);
        }

        var generator = new CSharpCodeGenerator();
        return generator.Generate(codeCompilation);
    }
}
