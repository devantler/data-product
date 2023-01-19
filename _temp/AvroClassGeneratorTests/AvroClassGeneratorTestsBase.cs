using Avro;

namespace Devantler.Commons.CodeGen.Tests.Unit.AvroClassGeneratorTests;

public class AvroClassGeneratorTestsBase
{
    internal readonly AvroClassGenerator _avroClassGenerator;

    public AvroClassGeneratorTestsBase() => _avroClassGenerator = new();

    internal Task Verify(string schemaPath)
    {
        //Arrange
        string schemaText = File.ReadAllText(schemaPath);
        var schema = Schema.Parse(schemaText);
        var recordSchema = (RecordSchema)schema;

        string @namespace = GetType().Name;

        //Act
        string code = _avroClassGenerator.Generate(@namespace, recordSchema);

        //Assert
        return Verifier.Verify(code).UseMethodName(schema.Name);
    }
}
