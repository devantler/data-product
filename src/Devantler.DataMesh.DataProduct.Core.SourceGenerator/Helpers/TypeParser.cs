namespace Devantler.DataMesh.DataProduct.Core.SourceGenerator.Helpers;

public static class TypeParser
{
    public static string Parse(string type, ModelType modelType)
    {
        var actualType = type.EndsWith("[]") ? type[..^2] : type;
        var parsedType = actualType switch
        {
            "number" => "int",
            "boolean" => "bool",
            "string" => "string",
            _ => $"{actualType}{(modelType == ModelType.Entity ? modelType : string.Empty)}"
        };
        return type.EndsWith("[]") ? $"List<{parsedType}>" : parsedType;
    }
    public enum ModelType
    {
        Entity,
        Model
    }

}
