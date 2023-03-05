namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.IncrementalGenerators.DbContextGeneratorTests;

public static class TestCases
{
    public static IEnumerable<object[]> ValidCases =>
        new List<object[]>
        {
            new object[]{"record-schema-primitive-types"},
        };

    public static IEnumerable<object[]> InvalidCases =>
        new List<object[]>();
}
