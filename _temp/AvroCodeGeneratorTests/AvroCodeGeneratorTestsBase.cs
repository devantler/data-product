using Avro;

namespace Devantler.Commons.CodeGen.Tests.Unit.AvroCodeGeneratorTests;

public class AvroCodeGeneratorTestsBase
{
    internal readonly AvroCodeGenerator _avroCodeGenerator;

    public AvroCodeGeneratorTestsBase() => _avroCodeGenerator = new();

    internal Task Verify(string schemaPath)
    {
        //Arrange
        string schemaText = File.ReadAllText(schemaPath);
        var schema = Schema.Parse(schemaText);

        string @namespace = GetType().Name;

        //Act
        string code = _avroCodeGenerator.Generate(@namespace, schema);

        //Assert
        return Verifier.Verify(code).UseMethodName(schema.Name);
    }
}
