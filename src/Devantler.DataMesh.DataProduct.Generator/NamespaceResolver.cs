using Microsoft.CodeAnalysis;

namespace Devantler.DataMesh.DataProduct.Generator;

/// <summary>
/// A resolver that can resolve namespaces from namespace symbols in the compilation object.
/// </summary>
public static class NamespaceResolver
{
    /// <summary>
    /// Resolves a namespace from a type.
    /// </summary>
    /// <param name="namespaceSymbol"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string ResolveForType(INamespaceSymbol namespaceSymbol, Type type) =>
        ResolveForType(namespaceSymbol, type.Name);

    /// <summary>
    /// Resolves a namespace from a types name.
    /// </summary>
    /// <param name="namespaceSymbol"></param>
    /// <param name="typeName"></param>
    /// <returns></returns>
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
