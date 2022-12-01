using System.Text;
using Devantler.DataMesh.DataProduct.Configuration;
using Devantler.DataMesh.DataProduct.Core.SourceGenerator.Helpers;
using Devantler.SourceGenerator.Core;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.Extensions.Configuration;
using static Devantler.DataMesh.DataProduct.Core.SourceGenerator.Helpers.TypeParser;

namespace Devantler.DataMesh.DataProduct.Core.SourceGenerator;

[Generator]
public class ModelsGenerator : GeneratorBase
{
    public override void Generate(SourceProductionContext context, Compilation compilation, IConfiguration configuration)
    {
        var schemas = configuration.GetSection("Schemas").Get<Schema[]>();
        var @namespace = $"{compilation.AssemblyName}.Models";

        foreach (var schema in schemas)
        {
            string source = ModelGenerator.GenerateModel(@namespace, schema, ModelType.Model);
            context.AddSource($"{schema.Name}.cs", SourceText.From(source, Encoding.UTF8));
        }
    }
}
