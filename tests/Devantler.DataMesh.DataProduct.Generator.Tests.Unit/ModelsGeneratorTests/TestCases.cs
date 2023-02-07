namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.ModelsGeneratorTests;

public static class TestCases
{
    public static IEnumerable<object[]> ValidCases =>
        new List<object[]>
        {
            new object[]{"RecordSchemaEmpty" },
            new object[]{"RecordSchemaNamespace"},
            new object[]{"RecordSchemaPrimitiveTypeBoolean"},
            new object[]{"RecordSchemaPrimitiveTypeBytes"},
            new object[]{"RecordSchemaPrimitiveTypeDouble"},
            new object[]{"RecordSchemaPrimitiveTypeFloat"},
            new object[]{"RecordSchemaPrimitiveTypeInt"},
            new object[]{"RecordSchemaPrimitiveTypeLong"},
            new object[]{"RecordSchemaPrimitiveTypeNull"},
            new object[]{"RecordSchemaPrimitiveTypeString"},
            new object[]{"RecordSchemaPrimitiveTypes"},
            new object[]{"EnumSchemaEmpty"},
            new object[]{"EnumSchemaNamespace"},
            new object[]{"EnumSchemaSymbols"},
            new object[]{"UnionSchemaEmpty"},
            new object[]{"UnionSchemaEnumSchema"},
            new object[]{"UnionSchemaEnumSchemas"},
            new object[]{"UnionSchemaMixedSchemas"},
            new object[]{"UnionSchemaRecordSchema"},
            new object[]{"UnionSchemaRecordSchemas"},
        };

    public static IEnumerable<object[]> InvalidCases =>
        new List<object[]>();
}
