using Avro;

namespace Devantler.DataMesh.AvroCodeGenerators.Tests.Unit.AvroEnumGeneratorTests;

public class AvroEnumGeneratorTestsBase
{
    internal readonly AvroEnumGenerator _avroEnumGenerator;

    public AvroEnumGeneratorTestsBase() => _avroEnumGenerator = new();

    internal Task Verify(string schemaPath)
    {
        //Arrange
        string schemaText = File.ReadAllText(schemaPath);
        Schema schema = Schema.Parse(schemaText);
        EnumSchema enumSchema = (EnumSchema)schema;

        string @namespace = GetType().Name;

        //Act
        string code = _avroEnumGenerator.Generate(@namespace, enumSchema);

        //Assert
        return Verifier.Verify(code).UseMethodName(schema.Name);
    }
}
