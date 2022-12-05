using System;
using Devantler.DataMesh.DataProduct.Configuration;
using Devantler.DataMesh.DataProduct.SourceGenerator.Core.Extensions;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Core.Parsers;

public static class SchemaParser
{
    public static string ParseToMappings(Schema[] schemas, string modelsNamespace, string entitiesNamespace)
    {
        var relativeModelsNamespace = modelsNamespace.Replace("Devantler.DataMesh.DataProduct.", "");
        var relativeEntitiesNamespace = entitiesNamespace.Replace("Devantler.DataMesh.DataProduct.", "");
        var source = "";
        for (var i = 0; i < schemas.Length; i++)
        {
            source += ParseToMapping(schemas[i], relativeModelsNamespace, relativeEntitiesNamespace);
            if (i < schemas.Length - 1)
                source += Environment.NewLine + Environment.NewLine;
        }

        return source;
    }

    public static string ParseToMapping(Schema schema, string relativeModelsNamespace, string relativeEntitiesNamespace)
    {
        return $$"""
        // {{schema.Name}} Mappings
        CreateMap<{{relativeEntitiesNamespace}}.{{schema.Name}}, {{relativeModelsNamespace}}.{{schema.Name}}>().ReverseMap();
        """;
    }

    public static string ParseToRepositoryServiceRegistrations(Schema[] schemas, string relativeEntitiesNamespace)
    {
        var source = "";
        for (var i = 0; i < schemas.Length; i++)
        {
            var schemaName = schemas[i].Name.ToPascalCase();
            var repositoryName = $"{schemaName.ToPlural()}Repository";
            source +=
            $$"""
            services.AddScoped<IRepository<{{relativeEntitiesNamespace}}.{{schemaName}}>, {{repositoryName}}>();
            """;
            if (i < schemas.Length - 1)
                source += Environment.NewLine;
        }

        return source;
    }
}
