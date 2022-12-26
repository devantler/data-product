using System;
using System.Linq;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Parsers;

public static class UsingsParser
{
    public static string Parse(string[] usings) =>
        string.Join(Environment.NewLine, usings.Select(x => $"using {x};"));
}
