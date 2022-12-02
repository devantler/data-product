using Devantler.DataMesh.DataProduct.SourceGenerator.Exceptions;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Parsers;

public static class ApiParser
{
    public static string Parse(string apiName)
    {
        return apiName.ToLower() switch
        {
            "rest" => "REST",
            "graphql" => "GraphQL",
            _ => throw new ParseException($"Unsupported API: {apiName}")
        };
    }
}
