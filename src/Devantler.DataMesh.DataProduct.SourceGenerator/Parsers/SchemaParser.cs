using System;
using Devantler.DataMesh.DataProduct.Configuration;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Parsers;

public static class SchemaParser
{
    public static string Parse(Schema[] schemas)
    {
        var source = "";
        for (int i = 0; i < schemas.Length; i++)
        {
            source +=
            $$"""
            // {{schemas[i].Name}} Mappings
            CreateMap<Features.DataStores.Entities.{{schemas[i].Name}}, Models.{{schemas[i].Name}}>().ReverseMap();
            CreateMap<IEnumerable<Features.DataStores.Entities.{{schemas[i].Name}}>, IEnumerable<Models.{{schemas[i].Name}}>>().ReverseMap();
            """;
            if(i < schemas.Length - 1)
                source += Environment.NewLine + Environment.NewLine;
            
        }

        return source;
    }
}
