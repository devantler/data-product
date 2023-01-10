namespace Devantler.DataMesh.AvroCodeGenerators.Tests.Unit.AvroClassGeneratorTests;

public class GenerateTests : AvroClassGeneratorTestsBase
{
    [Theory]
    [InlineData("assets/schemas/recordSchema-empty.avsc")]
    [InlineData("assets/schemas/recordSchema-primitiveType-boolean.avsc")]
    [InlineData("assets/schemas/recordSchema-primitiveType-bytes.avsc")]
    [InlineData("assets/schemas/recordSchema-primitiveType-double.avsc")]
    [InlineData("assets/schemas/recordSchema-primitiveType-float.avsc")]
    [InlineData("assets/schemas/recordSchema-primitiveType-int.avsc")]
    [InlineData("assets/schemas/recordSchema-primitiveType-long.avsc")]
    [InlineData("assets/schemas/recordSchema-primitiveType-null.avsc")]
    [InlineData("assets/schemas/recordSchema-primitiveType-string.avsc")]
    [InlineData("assets/schemas/recordSchema-primitiveTypes.avsc")]
    public Task Generate_WithValidRecordSchema_GeneratesValidClass(string schemaPath) =>
        VerifySchema(schemaPath);
}
