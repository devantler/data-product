using System;
using System.Collections.Generic;
using Avro;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Parsers;

public static class AvroFieldParser
{
    public static string Parse(List<Field> fields)
    {
        var source = "";
        for (var i = 0; i < fields.Count; i++)
        {
            source += Parse(fields[i]);
            if (i < fields.Count - 1)
                source += Environment.NewLine;
        }
        return source;
    }

    public static string Parse(Field field) =>
        $$"""
        public {{TypeParser.Parse(field.Schema.Name)}} {{field.Name}} { get; set; }
        """;
}
