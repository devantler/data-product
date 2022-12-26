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
// public class SqliteRepositoryGenerator : AppSettingsGenerator
// {
//     protected override void Generate(SourceProductionContext context, Compilation compilation, IConfiguration configuration)
//     {
//         var schemas = configuration.GetSection("Schemas").Get<Schema[]>();

//         var repositoriesNamespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "IRepository");
//         var entitiesNamespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "IEntity");

//         var usings = new string[] {
//             "Microsoft.EntityFrameworkCore",
//             NamespaceResolver.Resolve(compilation.GlobalNamespace, "RelationalDbContext"),
//             repositoriesNamespace,
//             entitiesNamespace
//         };

//         var @namespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "ISqliteRepository");

//         foreach (var schema in schemas)
//         {
//             var schemaName = schema.Name.ToPascalCase();
//             var className = $"{schemaName.ToPlural()}Repository";
//             var source =
//             $$"""
//             {{UsingsParser.Parse(usings)}}

//             namespace {{@namespace}};

//             public class {{className}} : EntityFrameworkRepository<{{schemaName}}>, ISqliteRepository
//             {
//                 public {{className}}(RelationalDbContext dbContext) : base(dbContext)
//                 {
//                 }
//             }

//             """;

//             context.AddSource($"{className}.cs", SourceText.From(source.AddMetadata(), Encoding.UTF8));
//         }
//     }
// }
