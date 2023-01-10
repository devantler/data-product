namespace Devantler.DataMesh.AvroCodeGenerators.Tests.Unit.AvroCodeGeneratorTests;

[UsesVerify]
public class GenerateTests : AvroCodeGeneratorTestsBase
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
    public Task Generate_WithValidRecordSchema_GeneratesValidCode(string schemaPath) =>
        VerifySchema(schemaPath);

    [Theory]
    [InlineData("assets/schemas/enumsSchema-empty.avsc")]
    [InlineData("assets/schemas/enumsSchema-symbols.avsc")]
    public Task Generate_WithValidEnumSchema_GeneratesValidCode(string schemaPath) =>
        VerifySchema(schemaPath);

    [Theory]
    [InlineData("assets/schemas/unionSchema-empty.avsc")]
    [InlineData("assets/schemas/unionSchema-enum.avsc")]
    [InlineData("assets/schemas/unionSchema-enums.avsc")]
    [InlineData("assets/schemas/unionSchema-recordSchema.avsc")]
    [InlineData("assets/schemas/unionSchema-recordSchemas.avsc")]
    [InlineData("assets/schemas/unionSchema-mixed.avsc")]
    public Task Generate_WithValidUnionSchema_GeneratesValidCode(string schemaPath) =>
        VerifySchema(schemaPath);
}
