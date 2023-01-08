using Microsoft.CodeAnalysis;

namespace Devantler.DataMesh.DataProduct.Generator;

public static class NamespaceResolver
{
    public static string ResolveForType(INamespaceSymbol namespaceSymbol, Type type) =>
        ResolveForType(namespaceSymbol, type.Name);

    public static string ResolveForType(INamespaceSymbol namespaceSymbol, string typeName)
    {
        // Find namespace for typeName recursively in namespaceSymbol or else throw exception
        foreach (INamespaceOrTypeSymbol member in namespaceSymbol.GetMembers())
        {
            if (member is INamespaceSymbol namespaceMember)
            {
                string @namespace = ResolveForType(namespaceMember, typeName);
                if (!string.IsNullOrEmpty(@namespace))
                    return @namespace;
            }
            else if (member is INamedTypeSymbol namedTypeMember && namedTypeMember.Name == typeName)
            {
                return namespaceSymbol.ToDisplayString();
            }
        }

        return string.Empty;
    }
}
