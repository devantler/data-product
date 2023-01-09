using System.CodeDom;
using Avro;
using Devantler.Commons.StringHelpers;

namespace Devantler.DataMesh.DataProduct.Generator;

/// <summary>
/// A generator that can generate code from Avro.Apache schemas.
/// </summary>
public class AvroCodeGenerator
{
    /// <summary>
    /// Generates a <see cref="CodeCompileUnit"/> from a <see cref="Schema"/>.
    /// </summary>
    /// <param name="namespace"></param>
    /// <param name="schema"></param>
    /// <returns></returns>
    public CodeCompileUnit Generate(string @namespace, Schema schema)
    {
        CodeCompileUnit codeCompileUnit = new();

        CodeNamespace codeNamespace = new(@namespace);
        CodeTypeDeclaration codeTypeDeclaration = new(schema.Name.ToPascalCase());
        _ = codeNamespace.Types.Add(codeTypeDeclaration);

        _ = codeCompileUnit.Namespaces.Add(codeNamespace);

        return schema switch
        {
            RecordSchema recordSchema => GenerateFromRecordSchema(codeCompileUnit, recordSchema),
            UnionSchema unionSchema => GenerateFromUnionSchema(codeCompileUnit, unionSchema),
            _ => throw new NotImplementedException($"A generator for the specified shema type {schema.GetType()} is not implemented yet.")
        };
    }

    CodeCompileUnit GenerateFromRecordSchema(CodeCompileUnit codeCompileUnit, RecordSchema recordSchema)
    {
        foreach (Field field in recordSchema.Fields)
        {
            CodeMemberProperty codeMemberProperty = new()
            {
                Name = field.Name.ToPascalCase(),
                Type = new CodeTypeReference(field.Schema.Name.ToPascalCase()),
                Attributes = MemberAttributes.Public
            };
            _ = codeCompileUnit.Namespaces[0].Types[0].Members.Add(codeMemberProperty);
        }

        return codeCompileUnit;
    }
    CodeCompileUnit GenerateFromUnionSchema(CodeCompileUnit codeCompileUnit, UnionSchema unionSchema) => throw new NotImplementedException();
}
