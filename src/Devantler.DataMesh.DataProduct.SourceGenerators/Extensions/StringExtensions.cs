using System;

namespace Devantler.DataMesh.DataProduct.SourceGenerators.Extensions;

public static class StringExtensions
{
    public static string IndentBy(this string text, int spaces = 4)
    {
        var indentation = new string(' ', spaces);
        return indentation + text.Replace(Environment.NewLine, Environment.NewLine + indentation);
    }
}
