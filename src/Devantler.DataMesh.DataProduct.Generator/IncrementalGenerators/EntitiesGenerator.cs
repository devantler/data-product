using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.Core;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.CSharp.Model;
using Devantler.Commons.CodeGen.Mapping.Avro;
using Devantler.Commons.CodeGen.Mapping.Avro.Extensions;
using Devantler.Commons.CodeGen.Mapping.Avro.Mappers;
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
        var schemaRegistryService = options.GetSchemaRegistryService();
        var rootSchema = schemaRegistryService.GetSchema(options.Schema.Subject, options.Schema.Version);

        var codeCompilation = new CSharpCompilation();

        foreach (var schema in rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>())
        {
            string schemaName = schema.Name.ToPascalCase();
            var @class = new CSharpClass($"{schemaName}Entity")
                .SetNamespace(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IEntity"))
                .AddImplementation(new CSharpInterface("IEntity"));

            var idProperty = new CSharpProperty("Guid", "Id");
            _ = @class.AddProperty(idProperty);

            foreach (var field in schema.Fields.Where(f => !string.Equals(f.Name, "id", StringComparison.OrdinalIgnoreCase)))
            {
                string propertyName = field.Name.ToPascalCase();
                string propertyType = AvroSchemaTypeParser.Parse(field, field.Type, Language.CSharp, Target.Entity);
                var property = field.Type switch
                {
                    RecordSchema => new CSharpProperty($"virtual {propertyType}", propertyName),
                    _ => new CSharpProperty(propertyType, propertyName)
                };

                _ = @class.AddProperty(property);
            }

            _ = codeCompilation.AddType(@class);
        }

        var generator = new CSharpCodeGenerator();
        return generator.Generate(codeCompilation);
    }
}
