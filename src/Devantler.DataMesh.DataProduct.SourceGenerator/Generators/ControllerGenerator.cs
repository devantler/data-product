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
// public class ControllerGenerator : AppSettingsGenerator
// {
//     protected override void Generate(SourceProductionContext context, Compilation compilation, IConfiguration configuration)
//     {
//         var apis = configuration.GetSection("Features").Get<Features>().Apis;
//         if (!apis.Contains("rest"))
//             return;

//         var schemas = configuration.GetSection("Schemas").Get<Schema[]>();

//         var modelsNamespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "IModel");
//         var entitiesNamespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "IEntity");

//         var usings = new string[] {
//             "AutoMapper",
//             "Microsoft.AspNetCore.Mvc",
//             modelsNamespace,
//             entitiesNamespace,
//             NamespaceResolver.Resolve(compilation.GlobalNamespace, "IRepository")
//         };

//         var rootNamespace = compilation.AssemblyName;
//         var relativeEntitiesNamespace = entitiesNamespace.Replace($"{rootNamespace}.", "");
//         var relativeModelsNamespace = modelsNamespace.Replace($"{rootNamespace}.", "");
//         var @namespace = NamespaceResolver.Resolve(compilation.GlobalNamespace, "IController");

//         foreach (var schema in schemas)
//         {
//             var schemaName = schema.Name.ToPascalCase();
//             var entity = $"{relativeEntitiesNamespace}.{schemaName}";
//             var model = $"{relativeModelsNamespace}.{schemaName}";
//             var @class = $"{schema.Name.ToPascalCase().ToPlural()}Controller";

//             var source =
//             $$"""
//             {{UsingsParser.Parse(usings)}}

//             namespace {{@namespace}};

//             [ApiController]
//             [Route("[controller]")]
//             [Produces("application/json")]
//             public class {{@class}} : ControllerBase, IController<{{model}}>
//             {
//                 private readonly IRepository<{{entity}}> _repository;
//                 private readonly IMapper _mapper;

//                 public {{@class}}(IRepository<{{entity}}> repository, IMapper mapper)
//                 {
//                     _repository = repository;
//                     _mapper = mapper;
//                 }

//                 /// <summary>
//                 /// Get one or more {{schemaName.ToPlural()}} by id, or paging.
//                 /// </summary>
//                 /// <param name="id">Ids of {{schemaName.ToPlural()}}.</param>
//                 /// <param name="page">The number of pages to return.</param>
//                 /// <param name="pageSize">The size of each page.</param>
//                 /// <param name="cancellationToken">A cancellation token.</param>
//                 /// <returns>A list of one or more {{schemaName.ToPlural()}}.</returns>
//                 [HttpGet]
//                 public async Task<ActionResult<IEnumerable<{{model}}>>> Get([FromQuery] IEnumerable<Guid> id, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
//                 {
//                     if (!id.Any())
//                     {
//                         var entities = await _repository.ReadPaged(page, pageSize, cancellationToken);
//                         var models = _mapper.Map<List<{{model}}>>(entities);
//                         return Ok(models);
//                     }
//                     else if (id.Count() == 1)
//                     {
//                         var entity = await _repository.Read(id.First(), cancellationToken);
//                         var model = _mapper.Map<{{model}}>(entity);
//                         return Ok(model);
//                     }
//                     else
//                     {
//                         var entities = await _repository.ReadMany(id, cancellationToken);
//                         var models = _mapper.Map<List<{{model}}>>(entities);
//                         return Ok(models);
//                     }
//                 }

//                 /// <summary>
//                 /// Post one or more {{schemaName.ToPlural()}}.
//                 /// </summary>
//                 /// <param name="models">One or more {{schemaName.ToPlural()}}.</param>
//                 /// <param name="cancellationToken">A cancellation token.</param>
//                 /// <returns>A list of ids for the posted {{schemaName.ToPlural()}}.</returns>
//                 [HttpPost]
//                 public async Task<ActionResult<IEnumerable<Guid>>> Post([FromBody] IEnumerable<{{model}}> models, CancellationToken cancellationToken = default)
//                 {
//                     return Ok();
//                 }
//             }

//             """;

//             context.AddSource($"{@class}.cs", SourceText.From(source, Encoding.UTF8));
//         }
//     }
// }
