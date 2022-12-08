// using System.Text;
// using Devantler.DataMesh.DataProduct.Configuration;
// using Devantler.DataMesh.DataProduct.SourceGenerator.Core;
// using Devantler.DataMesh.DataProduct.SourceGenerator.Core.Extensions;
// using Devantler.DataMesh.DataProduct.SourceGenerator.Core.Parsers;
// using Microsoft.CodeAnalysis;
// using Microsoft.CodeAnalysis.Text;
// using Microsoft.Extensions.Configuration;

// namespace Devantler.DataMesh.DataProduct.SourceGenerator.Generators;

// [Generator]
// public class MappingGenerator : AppSettingsGenerator
// {
//     protected override void Generate(SourceProductionContext context, Compilation compilation, IConfiguration configuration)
//     {
//         var schemas = configuration.GetSection("Schemas").Get<Schema[]>();

//         var rootNamespace = compilation.AssemblyName;
//         var modelsNamespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "IModel");
//         var entitiesNamespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "IEntity");
//         var repositoriesNamespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "IRepository");

//         var usings = new string[] {
//             "AutoMapper",
//             modelsNamespace,
//             entitiesNamespace,
//             repositoriesNamespace
//         };

//         var @namespace = $"{rootNamespace}.Mapping";
//         const string CLASS = "MappingProfile";

//         var source =
//         $$"""
//         {{UsingsParser.Parse(usings)}}

//         namespace {{@namespace}};

//         public class {{CLASS}} : Profile
//         {
//             public {{CLASS}}()
//             {
//                 {{SchemaParser.ParseToMappings(schemas, modelsNamespace, entitiesNamespace).IndentBy(8)}}
//             }
//         }
        
//         """;

//         context.AddSource($"{CLASS}.cs", SourceText.From(source.AddMetadata(), Encoding.UTF8));
//     }
// }
