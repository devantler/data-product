#pragma warning disable RCS1130
using System.CodeDom.Compiler;
using System.Text;
using Devantler.Commons.StringHelpers;
using Microsoft.CSharp;

namespace Devantler.DataMesh.AvroCodeGenerators.Extensions;

/// <summary>
/// A set of extension methods for <see cref="CodeCompileUnit"/> to make it more intuitive to use for code generation.
/// </summary>
public static class CodeCompileUnitExtensions
{
    /// <summary>
    /// Compiles a <see cref="CodeCompileUnit"/> to a string.
    /// </summary>
    /// <param name="codeCompileUnit"></param>
    /// <returns></returns>
    public static string Compile(this CodeCompileUnit codeCompileUnit)
    {
        CSharpCodeProvider codeProvider = new();
        CodeGeneratorOptions options = new()
        {
            BracingStyle = "C",
            BlankLinesBetweenMembers = false,
            VerbatimOrder = true,
            IndentString = "    ",
            ElseOnClosing = true
        };
        StringBuilder code = new();
        using (StringWriter writer = new(code))
        {
            codeProvider.GenerateCodeFromCompileUnit(codeCompileUnit, writer, options);
        }
        return code.ToString();
    }

    /// <summary>
    /// Adds a namespace to the <see cref="CodeCompileUnit"/>.
    /// </summary>
    /// <param name="codeCompileUnit"></param>
    /// <param name="namespace"></param>
    /// <returns></returns>
    public static CodeCompileUnit AddNamespace(this CodeCompileUnit codeCompileUnit, string @namespace)
    {
        _ = codeCompileUnit.Namespaces.Add(new(@namespace));
        return codeCompileUnit;
    }

    /// <summary>
    /// Adds a type to the first namespace in the <see cref="CodeCompileUnit"/>.
    /// </summary>
    /// <param name="codeCompileUnit"></param>
    /// <param name="type"></param>
    /// <param name="isEnum"></param>
    /// <param name="docBlock"></param>
    /// <returns></returns>
    public static CodeCompileUnit AddType(this CodeCompileUnit codeCompileUnit, string @type, bool isEnum, string? docBlock = null)
    {
        CodeTypeDeclaration codeTypeDeclaration = new(@type)
        {
            IsEnum = isEnum
        };

        _ = codeTypeDeclaration.Comments.Add(new CodeCommentStatement("<summary>", true));
        _ = codeTypeDeclaration.Comments.Add(new CodeCommentStatement(docBlock, true));
        _ = codeTypeDeclaration.Comments.Add(new CodeCommentStatement("</summary>", true));

        if (codeCompileUnit.Namespaces.Count == 0)
            throw new AvroCodeGeneratorException("No namespace has been added to the code compile unit.");

        _ = codeCompileUnit.Namespaces[0].Types.Add(codeTypeDeclaration);

        return codeCompileUnit;
    }

    /// <summary>
    /// Adds a property to the first type in the first namespace in the <see cref="CodeCompileUnit"/>.
    /// </summary>
    /// <param name="codeCompileUnit"></param>
    /// <param name="name"></param>
    /// <param name="type"></param>
    /// <param name="documentation"></param>
    /// <returns></returns>
    //TODO: Replace with auto-property solution if one is found.
    public static CodeCompileUnit AddPropertyWithBackingField(this CodeCompileUnit codeCompileUnit, string name, CodeTypeReference type, string? documentation = null)
    {
        if (codeCompileUnit.Namespaces.Count == 0)
            throw new AvroCodeGeneratorException("No namespace has been added to the code compile unit.");

        if (codeCompileUnit.Namespaces[0].Types.Count == 0)
            throw new InvalidOperationException("No type has been added to the code compile unit.");

        string fieldName = $"_{name.ToCamelCase()}";
        string propertyName = name.ToPascalCase();
        CodeMemberField field = CreateCodeMemberField(type, fieldName);
        CodeMemberProperty property = CreateCodeMemberProperty(propertyName, type, fieldName);

        _ = property.Comments.Add(new CodeCommentStatement("<summary>", true));
        _ = property.Comments.Add(new CodeCommentStatement(documentation, true));
        _ = property.Comments.Add(new CodeCommentStatement("</summary>", true));
        _ = codeCompileUnit.Namespaces[0].Types[0].Members.Add(field);
        _ = codeCompileUnit.Namespaces[0].Types[0].Members.Add(property);
        return codeCompileUnit;
    }

    static CodeMemberField CreateCodeMemberField(CodeTypeReference type, string fieldName) => new()
    {
        Attributes = MemberAttributes.Private,
        Name = fieldName,
        Type = type
    };

    static CodeMemberProperty CreateCodeMemberProperty(string propertyName, CodeTypeReference type, string fieldName) => new()
    {
        Attributes = MemberAttributes.Public | MemberAttributes.Final,
        Name = propertyName,
        Type = type,
        HasGet = true,
        HasSet = true,
        GetStatements = { CreateCodeMethodReturnStatement(fieldName) },
        SetStatements = { CreateCodeAssignmentStatement(fieldName) }
    };

    static CodeMethodReturnStatement CreateCodeMethodReturnStatement(string fieldName)
    {
        return new CodeMethodReturnStatement(
            new CodeFieldReferenceExpression(
                null!,
                fieldName
            )
        );
    }

    static CodeAssignStatement CreateCodeAssignmentStatement(string fieldName)
    {
        return new CodeAssignStatement(
            new CodeFieldReferenceExpression(
                null!,
                fieldName
            ),
            new CodePropertySetValueReferenceExpression()
        );
    }
}
