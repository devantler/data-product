namespace Devantler.DataMesh.DataProduct.Generator;

/// <summary>
/// A resolver that can resolve Apache.Avro primitive types to C# types.
/// </summary>
public static class AvroTypeResolver
{
    /// <summary>
    /// Resolves a primitive type name to a C# type.
    /// </summary>
    /// <param name="typeName"></param>
    /// <returns></returns>
    public static Type ResolveType(string typeName)
    {
        return typeName switch
        {
            "boolean" => typeof(bool),
            "int" => typeof(int),
            "long" => typeof(long),
            "float" => typeof(float),
            "double" => typeof(double),
            "bytes" => typeof(byte[]),
            "string" => typeof(string),
            _ => typeof(object)
        };
    }
}
