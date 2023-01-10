using System.CodeDom;
using Avro;
using Devantler.Commons.StringHelpers;
using Devantler.DataMesh.AvroCodeGenerators.Extensions;

namespace Devantler.DataMesh.AvroCodeGenerators;

/// <summary>
/// An Avro Code Generator that can generate a C# class from a <see cref="RecordSchema"/>.
/// </summary>
public class AvroClassGenerator : IAvroCodeGenerator<RecordSchema>
{
    /// <summary>
    /// Generates code from a <see cref="Schema" />.
    /// </summary>
    /// <param name="namespace"></param>
    /// <param name="schema"></param>
    /// <returns>The generated code as raw text.</returns>
    public string Generate(string @namespace, RecordSchema schema)
    {
        CodeCompileUnit compileUnit = new();
        _ = compileUnit.AddNamespace(@namespace);
        _ = compileUnit.AddType(schema.Name, schema.Documentation);

        if (!schema.Fields.Any(x => x.Name.Equals("id", StringComparison.OrdinalIgnoreCase)))
            _ = compileUnit.AddPropertyWithBackingField("Id", new CodeTypeReference(typeof(Guid)), "The unique identifier of the model.");

        for (int i = 0; i < schema.Fields.Count; i++)
        {
            Type type = AvroTypeResolver.ResolveType(schema.Fields[i].Schema.Name);
            CodeTypeReference codeTypeReference = type.GetType() == typeof(object) ?
                (new(schema.Fields[i].Schema.Name.ToPascalCase())) :
                (new(type));
            _ = compileUnit.AddPropertyWithBackingField(
                schema.Fields[i].Name,
                codeTypeReference,
                schema.Fields[i].Documentation
            );
        }

        return compileUnit.Compile();
    }
}
