using Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.EntitiesGeneratorTests;

[UsesVerify]
public class GenerateTests : IncrementalGeneratorTestsBase<EntitiesGenerator>
{
    [Theory]
    [MemberData(nameof(TestCases.ValidCases), MemberType = typeof(TestCases))]
    public Task GivenValidAppSettings_GeneratesValidCode(string subject)
    {
        //Arrange
        var additionalText = CreateAppSettingsWithLocalSchemaRegistryAndSchema(subject);

        //Assert
        return Verify(additionalText).UseMethodName(subject);
    }
}
