namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.IncrementalGenerators.SchemaGeneratorTests;

public static class TestCases
{
    public static IEnumerable<object[]> ValidCases =>
        new List<object[]>
        {
            new object[]{"record-schema-empty" },
            new object[]{"record-schema-namespace"},
            new object[]{"record-schema-primitive-type-boolean"},
            new object[]{"record-schema-primitive-type-bytes"},
            new object[]{"record-schema-primitive-type-double"},
            new object[]{"record-schema-primitive-type-float"},
            new object[]{"record-schema-primitive-type-int"},
            new object[]{"record-schema-primitive-type-long"},
            new object[]{"record-schema-primitive-type-null"},
            new object[]{"record-schema-primitive-type-string"},
            new object[]{"record-schema-primitive-types"},
            new object[]{"enum-schema-empty"},
            new object[]{"enum-schema-namespace"},
            new object[]{"enum-schema-symbols"},
            new object[]{"union-schema-empty"},
            new object[]{"union-schema-enum-schema"},
            new object[]{"union-schema-enum-schemas"},
            new object[]{"union-schema-mixed-schemas"},
            new object[]{"union-schema-record-schema"},
            new object[]{"union-schema-record-schemas"}
        };

    public static IEnumerable<object[]> InvalidCases =>
        new List<object[]>();
}
