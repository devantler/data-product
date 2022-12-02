using System.Collections.Generic;
using System.Text;
using static Devantler.DataMesh.DataProduct.Configuration.Schema;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Parsers;

public static class PropertyParser
{
    public static string Parse(List<Property> properties)
    {
        var source = new StringBuilder();
        foreach (var property in properties)
        {
            var type = TypeParser.Parse(property.Type);
            source.AppendLine($"public {type} {property.Name} {{ get; set; }}");
        }
        return source.ToString();
    }
}
