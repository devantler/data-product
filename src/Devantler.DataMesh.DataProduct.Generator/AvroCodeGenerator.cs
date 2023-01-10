#pragma warning disable RCS1130
using System.CodeDom;
using Avro;
using Devantler.Commons.StringHelpers;

namespace Devantler.DataMesh.DataProduct.Generator;

/// <summary>
/// A generator that can generate code from Apache.Avro schemas.
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
        _ = codeTypeDeclaration.Comments.Add(new CodeCommentStatement($"The {schema.Name.ToPascalCase()} model.", true));
        _ = codeNamespace.Types.Add(codeTypeDeclaration);
        _ = codeCompileUnit.Namespaces.Add(codeNamespace);

        return schema switch
        {
            RecordSchema recordSchema => GenerateFromRecordSchema(codeCompileUnit, recordSchema),
            _ => throw new NotImplementedException($"A generator for the specified shema type {schema.GetType()} is not implemented yet.")
        };
    }

    CodeCompileUnit GenerateFromRecordSchema(CodeCompileUnit codeCompileUnit, RecordSchema recordSchema)
    {
        if (!recordSchema.Fields.Any(x => x.Name.Equals("id", StringComparison.OrdinalIgnoreCase)))
            GenerateAutoProperty(codeCompileUnit, "Id", new CodeTypeReference(typeof(Guid)), "The unique identifier of the model.");

        for (int i = 0; i < recordSchema.Fields.Count; i++)
        {
            Type type = AvroTypeResolver.ResolveType(recordSchema.Fields[i].Schema.Name);
            CodeTypeReference codeTypeReference = type.GetType() == typeof(object) ?
                (new(recordSchema.Fields[i].Schema.Name.ToPascalCase())) :
                (new(type));
            GenerateAutoProperty(
                codeCompileUnit,
                recordSchema.Fields[i].Name,
                codeTypeReference,
                recordSchema.Fields[i].Documentation
            );
        }

        return codeCompileUnit;
    }

    static void GenerateAutoProperty(CodeCompileUnit codeCompileUnit, string propertyName, CodeTypeReference type, string? documentation = null)
    {
        string fieldName = $"_{propertyName.ToCamelCase()}";
        propertyName = propertyName.ToPascalCase();
        CodeMemberField idCodeMemberField = CreateCodeMemberField(type, fieldName);
        CodeMemberProperty idCodeMemberProperty = CreateCodeMemberProperty(propertyName, type, documentation, fieldName);

        _ = codeCompileUnit.Namespaces[0].Types[0].Members.Add(idCodeMemberField);
        _ = codeCompileUnit.Namespaces[0].Types[0].Members.Add(idCodeMemberProperty);
    }

    static CodeMemberField CreateCodeMemberField(CodeTypeReference type, string fieldName)
    {
        return new()
        {
            Attributes = MemberAttributes.Private,
            Name = fieldName,
            Type = type
        };
    }

    static CodeMemberProperty CreateCodeMemberProperty(string propertyName, CodeTypeReference type, string? documentation, string fieldName)
    {
        return new()
        {
            Attributes = MemberAttributes.Public | MemberAttributes.Final,
            Name = propertyName,
            Type = type,
            Comments = { new CodeCommentStatement(documentation ?? "", true) },
            HasGet = true,
            HasSet = true,
            GetStatements = { CreateCodeMethodReturnStatement(fieldName) },
            SetStatements = { CreateCodeAssignmentStatement(fieldName) }
        };
    }

    static CodeMethodReturnStatement CreateCodeMethodReturnStatement(string fieldName)
    {
        return new CodeMethodReturnStatement(
            new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(),
                fieldName
            )
        );
    }

    static CodeAssignStatement CreateCodeAssignmentStatement(string fieldName)
    {
        return new CodeAssignStatement(
            new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(),
                fieldName
            ),
            new CodePropertySetValueReferenceExpression()
        );
    }
}
