using Devantler.DataMesh.DataProduct.Generator.Generators;

namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.Generators;

[UsesVerify]
public class ModelsGeneratorTests : GeneratorTestsBase<ModelsGenerator>
{
    [Fact]
    public Task Generate_WithLocalSchemaRegistryAndRecordSchema_GeneratesASingleModel() =>
        Verify(new[] { "assets/appsettings.local-schema-registry-with-record-schema.json" });
}
