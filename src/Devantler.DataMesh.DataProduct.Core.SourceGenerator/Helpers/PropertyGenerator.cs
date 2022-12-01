using System;
using System.Linq;
using Devantler.DataMesh.DataProduct.Configuration;
using static Devantler.DataMesh.DataProduct.Core.SourceGenerator.Helpers.TypeParser;

namespace Devantler.DataMesh.DataProduct.Core.SourceGenerator.Helpers;

public static class PropertyGenerator
{
    public static string GenerateProperties(Schema schema, ModelType modelType)
    {
        return string.Join(
            Environment.NewLine,
            schema.Properties.Select(p => GenerateProperty(p, modelType))
        );
    }

    public static string GenerateProperty(Schema.Property property, ModelType modelType) =>
        $"public {Parse(property.Type, modelType)} {property.Name} {{ get; set; }}";
}
