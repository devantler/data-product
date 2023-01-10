using Devantler.Commons.StringHelpers;

namespace Devantler.DataMesh.AvroCodeGenerators;

/// <summary>
/// An Avro Code Generator that can generate a C# enum from a <see cref="EnumSchema"/>.
/// </summary>
public class AvroEnumGenerator : IAvroCodeGenerator<EnumSchema>
{
    /// <inheritdoc/>
    public string Generate(string @namespace, EnumSchema schema)
    {
        CodeCompileUnit compileUnit = new();
        _ = compileUnit.AddNamespace(@namespace);
        _ = compileUnit.AddType(schema.Name, true, schema.Documentation);

        foreach (string? symbol in schema.Symbols)
        {
            CodeMemberField field = new(compileUnit.Namespaces[0].Types[0].Name, symbol.ToPascalCase());
            _ = compileUnit.Namespaces[0].Types[0].Members.Add(field);
        }

        return compileUnit.Compile();
    }
}
