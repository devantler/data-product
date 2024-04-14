using Microsoft.CodeAnalysis;

namespace Devantler.DataProduct.Generator;

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
  /// <returns>The namespace for the given type</returns>
  public static string ResolveForType(INamespaceSymbol namespaceSymbol, Type type) =>
      ResolveForType(namespaceSymbol, type.Name);

  /// <summary>
  /// Resolves a namespace from a type's name.
  /// </summary>
  /// <param name="namespaceSymbol"></param>
  /// <param name="typeName"></param>
  /// <returns>The namespace for the given type name</returns>
  public static string ResolveForType(INamespaceSymbol namespaceSymbol, string typeName)
  {
    foreach (var member in namespaceSymbol.GetMembers())
    {
      switch (member)
      {
        case INamespaceSymbol namespaceMember:
          {
            string @namespace = ResolveForType(namespaceMember, typeName);
            if (!string.IsNullOrEmpty(@namespace))
              return @namespace;
            break;
          }
        case INamedTypeSymbol namedTypeMember when namedTypeMember.Name == typeName:
          return namespaceSymbol.ToDisplayString();
        default:
          continue;
      }
    }

    return string.Empty;
  }
}
