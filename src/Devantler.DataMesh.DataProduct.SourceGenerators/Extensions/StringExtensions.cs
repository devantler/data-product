namespace Devantler.DataMesh.DataProduct.SourceGenerators.Extensions;

public static class StringExtensions
{
    public static string IndentBy(this string text, int indentLevel)
    {
        var indentation = new string(' ', indentLevel);
        return indentation + text;
    }
}
