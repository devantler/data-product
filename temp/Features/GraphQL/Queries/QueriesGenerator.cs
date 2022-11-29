using Devantler.DataMesh.DataProduct.SourceGenerator.Base;
using Devantler.DataMesh.DataProduct.SourceGenerator.Models;
using Microsoft.CodeAnalysis;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Generators.Features.GraphQL.Queries;

[Generator]
public class QueriesGenerator : GeneratorBase, IIncrementalGenerator
{
    public override void Generate(SourceProductionContext context, Compilation compilation, Configuration configuration)
    {
    }
}
