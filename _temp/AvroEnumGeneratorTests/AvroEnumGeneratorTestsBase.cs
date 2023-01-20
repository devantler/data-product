using Avro;

namespace Devantler.Commons.CodeGen.Tests.Unit.AvroEnumGeneratorTests;

public class AvroEnumGeneratorTestsBase
{
    internal readonly AvroEnumGenerator _avroEnumGenerator;

    public AvroEnumGeneratorTestsBase() => _avroEnumGenerator = new();

    internal Task Verify(string schemaPath)
    {
        //Arrange
        string schemaText = File.ReadAllText(schemaPath);
        var schema = Schema.Parse(schemaText);
        var enumSchema = (EnumSchema)schema;

        string @namespace = GetType().Name;

        //Act
        string code = _avroEnumGenerator.Generate(@namespace, enumSchema);

        //Assert
        return Verifier.Verify(code).UseMethodName(schema.Name);
    }
}
