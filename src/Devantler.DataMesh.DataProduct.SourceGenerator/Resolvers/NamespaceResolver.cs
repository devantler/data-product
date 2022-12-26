using System.Linq;
using Microsoft.CodeAnalysis;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Resolvers;

public static class NamespaceResolver
{
    public static string Resolve(INamespaceSymbol namespaceSymbol, string typeName)
    {
        var typeSymbol = namespaceSymbol.GetTypeMembers(typeName).FirstOrDefault();
        if (typeSymbol != null)
        {
            return namespaceSymbol.ToDisplayString();
        }

        foreach (var ns in namespaceSymbol.GetNamespaceMembers())
        {
            var result = Resolve(ns, typeName);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }
}
