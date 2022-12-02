using System;
using System.Collections.Generic;
using static Devantler.DataMesh.DataProduct.Configuration.Schema;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Parsers;

public static class PropertyParser
{
    public static string Parse(List<Property> properties)
    {
        var source = "";
        for (int i = 0; i < properties.Count; i++)
        {
            source +=
            $$"""
            public {{TypeParser.Parse(properties[i].Type)}} {{properties[i].Name}} { get; set; }
            """;
            if (i < properties.Count - 1)
                source += Environment.NewLine;

        }
        return source.ToString();
    }
}
