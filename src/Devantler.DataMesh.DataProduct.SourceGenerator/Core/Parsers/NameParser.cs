using Devantler.DataMesh.DataProduct.SourceGenerator.Core.Exceptions;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Core.Parsers;

public static class NameParser
{
    public static string ParseApiName(string apiName)
    {
        return apiName.ToLower() switch
        {
            "rest" => "Rest",
            "graphql" => "GraphQl",
            _ => throw new ParseException($"Unsupported API: {apiName}")
        };
    }
}
