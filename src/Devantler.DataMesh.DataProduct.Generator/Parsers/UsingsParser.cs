namespace Devantler.DataMesh.DataProduct.Generator.Parsers;

public static class UsingsParser
{
    public static string Parse(string[] usings) =>
        string.Join(Environment.NewLine, usings.Select(x => $"using {x};"));
}
