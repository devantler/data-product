namespace Devantler.Commons.CodeGen.Tests.Unit.AvroEnumGeneratorTests;

[UsesVerify]
public class GenerateTests : AvroEnumGeneratorTestsBase
{
    [Theory]
    [InlineData("schemas/enum-schema-empty-v1.avsc")]
    [InlineData("schemas/enum-schema-symbols-v1.avsc")]
    public Task GivenValidEnumSchema_GenerateValidCode(string schemaPath) =>
        Verify(schemaPath);
}
