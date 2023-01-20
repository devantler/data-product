namespace Devantler.Commons.CodeGen.Tests.Unit.AvroClassGeneratorTests;

[UsesVerify]
public class GenerateTests : AvroClassGeneratorTestsBase
{
    [Theory]
    [InlineData("schemas/record-schema-empty-v1.avsc")]
    [InlineData("schemas/record-schema-primitive-type-boolean-v1.avsc")]
    [InlineData("schemas/record-schema-primitive-type-bytes-v1.avsc")]
    [InlineData("schemas/record-schema-primitive-type-double-v1.avsc")]
    [InlineData("schemas/record-schema-primitive-type-float-v1.avsc")]
    [InlineData("schemas/record-schema-primitive-type-int-v1.avsc")]
    [InlineData("schemas/record-schema-primitive-type-long-v1.avsc")]
    [InlineData("schemas/record-schema-primitive-type-null-v1.avsc")]
    [InlineData("schemas/record-schema-primitive-type-string-v1.avsc")]
    [InlineData("schemas/record-schema-primitive-types-v1.avsc")]
    public Task GivenValidRecordSchema_GenerateValidClass(string schemaPath) =>
        Verify(schemaPath);
}
