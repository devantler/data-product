using Devantler.DataMesh.DataProduct.SourceGenerators.Base;
using Devantler.DataMesh.DataProduct.SourceGenerators.Models;
using Microsoft.CodeAnalysis;

namespace Devantler.DataMesh.DataProduct.SourceGenerators.Generators.Features.GraphQL.Queries;

[Generator]
public class QueriesGenerator : GeneratorBase, IIncrementalGenerator
{
    public override void Generate(SourceProductionContext context, Compilation compilation, Configuration configuration)
    {
    }
}
