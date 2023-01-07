using Devantler.DataMesh.DataProduct.Generator.Exceptions;

namespace Devantler.DataMesh.DataProduct.Generator.Parsers;

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
