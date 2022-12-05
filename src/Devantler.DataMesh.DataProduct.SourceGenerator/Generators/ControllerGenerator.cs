using System.Text;
using Devantler.DataMesh.DataProduct.Configuration;
using Devantler.DataMesh.DataProduct.SourceGenerator.Core;
using Devantler.DataMesh.DataProduct.SourceGenerator.Core.Extensions;
using Devantler.DataMesh.DataProduct.SourceGenerator.Core.Parsers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.Extensions.Configuration;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Generators;

[Generator]
public class ControllerGenerator : AppSettingsGenerator
{
    protected override void Generate(SourceProductionContext context, Compilation compilation, IConfiguration configuration)
    {
        var apis = configuration.GetSection("Features").Get<Features>().Apis;
        if (!apis.Contains("rest"))
            return;

        var schemas = configuration.GetSection("Schemas").Get<Schema[]>();

        var modelsNamespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "IModel");
        var entitiesNamespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "IEntity");

        var usings = new string[] {
            "AutoMapper",
            "Microsoft.AspNetCore.Mvc",
            modelsNamespace,
            entitiesNamespace,
            NamespaceResolver.Resolve(compilation.GlobalNamespace, "IRepository")
        };

        var rootNamespace = compilation.AssemblyName;
        var relativeEntitiesNamespace = entitiesNamespace.Replace($"{rootNamespace}.", "");
        var relativeModelsNamespace = modelsNamespace.Replace($"{rootNamespace}.", "");
        var @namespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "IController");

        foreach (var schema in schemas)
        {
            var schemaName = schema.Name.ToPascalCase();
            var @class = $"{schema.Name.ToPascalCase().ToPlural()}Controller";

            var source =
            $$"""
            {{UsingsParser.Parse(usings)}}
            
            namespace {{@namespace}};
            
            public class {{@class}} : ControllerBase<{{relativeModelsNamespace}}.{{schemaName}}, {{relativeEntitiesNamespace}}.{{schemaName}}>
            {
                public {{@class}}(IRepository<{{relativeEntitiesNamespace}}.{{schemaName}}> repository, IMapper mapper) : base(repository, mapper)
                {
                }
            }
            
            """;

            context.AddSource($"{@class}.cs", SourceText.From(source.AddMetadata(), Encoding.UTF8));
        }
    }
}
