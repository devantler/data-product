namespace Devantler.DataMesh.DataProduct.SourceGenerator.Parsers;

public static class TypeParser
{
    public static string Parse(string type)
    {
        var actualType = type.EndsWith("[]") ? type[..^2] : type;
        var parsedType = actualType switch
        {
            "number" => "int",
            "boolean" => "bool",
            "string" => "string",
            _ => $"{actualType}"
        };
        return type.EndsWith("[]") ? $"List<{parsedType}>" : parsedType;
    }
}
