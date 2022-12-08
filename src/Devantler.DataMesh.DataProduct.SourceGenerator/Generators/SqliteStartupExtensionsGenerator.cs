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
// public class SqliteStartupExtensionsGenerator : AppSettingsGenerator
// {
//     protected override void Generate(SourceProductionContext context, Compilation compilation, IConfiguration configuration)
//     {
//         var schemas = configuration.GetSection("Schemas").Get<Schema[]>();
//         var dataStoreProvider = configuration.GetSection("Features").Get<Features>().DataStoreProvider;

//         var repositoriesNamespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "IRepository");
//         var sqliteRepositoriesNamespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "ISqliteRepository");
//         var relationalDbContextsNamespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "RelationalDbContext");

//         var usings = new string[] {
//             repositoriesNamespace,
//             sqliteRepositoriesNamespace,
//             relationalDbContextsNamespace
//         };

//         var relativeEntitiesNamespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "IEntity").Replace($"{compilation.AssemblyName}.", "");
//         var @namespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "SqliteStartupExtensions");
//         const string CLASS_NAME = "SqliteStartupExtensions";

//         var source =
//         $$"""
//         {{UsingsParser.Parse(usings)}}

//         namespace {{@namespace}};

//         public static partial class {{CLASS_NAME}}
//         {
//             static partial void GenerateServiceRegistrations(IServiceCollection services) 
//             {
//                 services.AddSqlite<RelationalDbContext>($"Data Source={{dataStoreProvider.ToString().ToLower()}}.db");
//                 {{SchemaParser.ParseToRepositoryServiceRegistrations(schemas, relativeEntitiesNamespace).IndentBy(8)}}
//             }

//             static partial void GenerateMiddlewareRegistrations(WebApplication app)
//             {

//             }
//         }

//         """;

//         context.AddSource($"{CLASS_NAME}.cs", SourceText.From(source.AddMetadata(), Encoding.UTF8));
//     }
// }
