using Avro;

namespace Devantler.DataMesh.AvroCodeGenerators.Tests.Unit.AvroClassGeneratorTests;

public class AvroClassGeneratorTestsBase
{
    internal readonly AvroClassGenerator _avroClassGenerator;

    public AvroClassGeneratorTestsBase() => _avroClassGenerator = new();

    internal Task VerifySchema(string schemaPath)
    {
        //Arrange
        string schemaText = File.ReadAllText(schemaPath);
        Schema schema = Schema.Parse(schemaText);
        RecordSchema recordSchema = (RecordSchema)schema;
        string @namespace = GetType().Name;

        //Act
        string code = _avroClassGenerator.Generate(@namespace, recordSchema);

        //Assert
        return Verify(code).UseMethodName(schema.Name);
    }
}
