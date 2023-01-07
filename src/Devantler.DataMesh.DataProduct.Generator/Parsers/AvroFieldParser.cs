using Avro;
using Devantler.Commons.StringHelpers;

namespace Devantler.DataMesh.DataProduct.Generator.Parsers;

public static class AvroFieldParser
{
    public static string Parse(List<Field> fields, int indentation = 0)
    {
        var source = "";
        for (var i = 0; i < fields.Count; i++)
        {
            if (i == 0)
                source += Parse(fields[i]) + Environment.NewLine;
            else if (i < fields.Count - 1)
                source += Parse(fields[i]).Indent(indentation) + Environment.NewLine;
            else
                source += Parse(fields[i]).Indent(indentation);
        }
        return source;
    }

    public static string Parse(Field field) =>
        $$"""
        public {{TypeParser.Parse(field.Schema.Name)}} {{field.Name}} { get; set; }
        """;
}
