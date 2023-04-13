using System.Runtime.CompilerServices;

namespace Devantler.DataProduct.Generator.Tests.Unit;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init() => VerifySourceGenerators.Initialize();
}
