using Devantler.DataMesh.DataProduct.Configuration;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Parsers;

public static class SchemaParser
{
    public static string Parse(Schema[] schemas)
    {
        var source = "";
        foreach (var schema in schemas)
        {
            source +=
            $$"""
            CreateMap<Entities.{{schema.Name}}, Models.{{schema.Name}}>().ReverseMap();
            CreateMap<IEnumerable<Entities.{{schema.Name}}>, IEnumerable<Models.{{schema.Name}}>>().ReverseMap();
            """;
        }

        return source;
    }
}
