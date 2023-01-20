namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.ModelsGeneratorTests;

[UsesVerify]
public class GenerateTests : ModelsGeneratorTestsBase
{
    [Theory]
    [InlineData("RecordSchemaEmpty")]
    [InlineData("RecordSchemaNamespace")]
    [InlineData("RecordSchemaPrimitiveTypeBoolean")]
    [InlineData("RecordSchemaPrimitiveTypeBytes")]
    [InlineData("RecordSchemaPrimitiveTypeDouble")]
    [InlineData("RecordSchemaPrimitiveTypeFloat")]
    [InlineData("RecordSchemaPrimitiveTypeInt")]
    [InlineData("RecordSchemaPrimitiveTypeLong")]
    [InlineData("RecordSchemaPrimitiveTypeNull")]
    [InlineData("RecordSchemaPrimitiveTypeString")]
    [InlineData("RecordSchemaPrimitiveTypes")]
    public Task GivenValidAppSettingsWithRecordSchema_GenerateValidCode(string subject)
    {
        //Arrange
        var additionalText = CreateAppSettingsWithLocalSchemaRegistryAndSchema(subject);

        //Act
        //Assert
        return Verify(additionalText);
    }

    [Theory]
    [InlineData("EnumSchemaEmpty")]
    [InlineData("EnumSchemaNamespace")]
    [InlineData("EnumSchemaSymbols")]
    public Task GivenValidAppSettingsWithEnumSchema_GenerateValidCode(string subject)
    {
        //Arrange
        var additionalText = CreateAppSettingsWithLocalSchemaRegistryAndSchema(subject);

        //Act
        //Assert
        return Verify(additionalText);
    }

    [Theory]
    [InlineData("UnionSchemaEmpty")]
    [InlineData("UnionSchemaEnumSchema")]
    [InlineData("UnionSchemaEnumSchemas")]
    [InlineData("UnionSchemaMixedSchemas")]
    [InlineData("UnionSchemaRecordSchema")]
    [InlineData("UnionSchemaRecordSchemas")]
    public Task GivenValidAppSettingsWithUnionSchema_GenerateValidCode(string subject)
    {
        //Arrange
        var additionalText = CreateAppSettingsWithLocalSchemaRegistryAndSchema(subject);

        //Act
        //Assert
        return Verify(additionalText);
    }
}
