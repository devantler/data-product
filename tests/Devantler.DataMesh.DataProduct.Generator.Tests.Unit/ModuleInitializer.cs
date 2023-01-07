using System.Runtime.CompilerServices;

namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init() => VerifySourceGenerators.Enable();
}
