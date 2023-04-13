using Devantler.DataProduct.Generator.IncrementalGenerators;

namespace Devantler.DataProduct.Generator.Tests.Unit.IncrementalGenerators.CachingStartupExtensionsGeneratorTests;

[UsesVerify]
public class GenerateTests : IncrementalGeneratorTestsBase<CachingStartupExtensionsGenerator>
{
    [Theory]
    [MemberData(nameof(TestCases.ValidCases), MemberType = typeof(TestCases))]
    public Task GivenValidDataProductConfig_GeneratesValidCode(string subject)
    {
        //Arrange
        var additionalText = CreateDataProductConfig(
            $$"""
            {
                "FeatureFlags": {
                    "EnableCaching": true
                },
                "SchemaRegistry": {
                    "Type": "Local",
                    "Path": "schemas",
                    "Schema": {
                        "Subject": "{{subject}}",
                        "Version": 1
                    }
                },
                "CacheStore": {
                    "Type": "InMemory"
                }
            }
            """
        );

        //Act
        var driver = RunGenerator(additionalText);

        //Assert
        return Verify(driver).UseMethodName(subject).DisableRequireUniquePrefix();
    }
}
