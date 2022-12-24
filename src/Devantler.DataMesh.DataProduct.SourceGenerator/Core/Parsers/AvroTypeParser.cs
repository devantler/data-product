using System;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Core.Parsers;

public static class AvroTypeParser
{
    internal static string Parse(Avro.Schema.Type type)
    {
        return type switch
        {
            Avro.Schema.Type.Null => "object",
            Avro.Schema.Type.Boolean => "bool",
            Avro.Schema.Type.Int => "int",
            Avro.Schema.Type.Long => "long",
            Avro.Schema.Type.Float => "float",
            Avro.Schema.Type.Double => "double",
            Avro.Schema.Type.Bytes => "byte[]",
            Avro.Schema.Type.String => "string",
            Avro.Schema.Type.Record => throw new NotImplementedException(),
            Avro.Schema.Type.Enumeration => throw new NotImplementedException(),
            Avro.Schema.Type.Array => throw new NotImplementedException(),
            Avro.Schema.Type.Map => throw new NotImplementedException(),
            Avro.Schema.Type.Union => throw new NotImplementedException(),
            Avro.Schema.Type.Fixed => throw new NotImplementedException(),
            Avro.Schema.Type.Error => throw new NotImplementedException(),
            Avro.Schema.Type.Logical => throw new NotImplementedException(),
            _ => throw new NotImplementedException(),
        };
    }
}
