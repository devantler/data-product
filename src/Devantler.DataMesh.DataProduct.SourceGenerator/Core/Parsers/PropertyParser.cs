using System;
using System.Collections.Generic;
using static Devantler.DataMesh.DataProduct.Configuration.Schema;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Core.Parsers;

public static class PropertyParser
{
    public static string Parse(List<Property> properties)
    {
        var source = "";
        for (var i = 0; i < properties.Count; i++)
        {
            source += Parse(properties[i]);
            if (i < properties.Count - 1)
                source += Environment.NewLine;
        }
        return source;
    }

    public static string Parse(Property property) =>
        $$"""
        public {{TypeParser.Parse(property.Type)}} {{property.Name}} { get; set; }
        """;
}
