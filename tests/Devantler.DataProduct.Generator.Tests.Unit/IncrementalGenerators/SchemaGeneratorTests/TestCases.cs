namespace Devantler.DataProduct.Generator.Tests.Unit.IncrementalGenerators.SchemaGeneratorTests;

public static class TestCases
{
  public static IEnumerable<object[]> ValidCases =>
    [
        ["record-schema-empty"],
        ["record-schema-namespace"],
        ["record-schema-primitive-type-boolean"],
        ["record-schema-primitive-type-bytes"],
        ["record-schema-primitive-type-double"],
        ["record-schema-primitive-type-float"],
        ["record-schema-primitive-type-int"],
        ["record-schema-primitive-type-long"],
        ["record-schema-primitive-type-null"],
        ["record-schema-primitive-type-string"],
        ["record-schema-primitive-types"],
        ["enum-schema-empty"],
        ["enum-schema-namespace"],
        ["enum-schema-symbols"],
        ["union-schema-empty"],
        ["union-schema-enum-schema"],
        ["union-schema-enum-schemas"],
        ["union-schema-mixed-schemas"],
        ["union-schema-record-schema"],
        ["union-schema-record-schemas"]
    ];
}
