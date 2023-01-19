using Devantler.DataMesh.DataProduct.SourceGenerator.Base;
using Microsoft.CodeAnalysis;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Generators.GraphQL.Queries;

[Generator]
public class QueriesGenerator : GeneratorBase, IIncrementalGenerator
{
    public override void Generate(SourceProductionContext context, Compilation compilation, Configuration configuration)
    {
    }
}
