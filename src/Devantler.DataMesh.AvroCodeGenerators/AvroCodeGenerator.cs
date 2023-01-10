namespace Devantler.DataMesh.AvroCodeGenerators;

/// <summary>
/// An avro code generator that can generate C# code from <see cref="Schema"/>.
/// </summary>
public class AvroCodeGenerator : IAvroCodeGenerator<Schema>
{
    /// <summary>
    /// Generates a <see cref="CodeCompileUnit"/> from a <see cref="Schema"/>.
    /// </summary>
    /// <param name="namespace"></param>
    /// <param name="schema"></param>
    /// <returns></returns>
    public string Generate(string @namespace, Schema schema)
    {
        AvroClassGenerator avroClassGenerator = new();
        AvroEnumGenerator avroEnumGenerator = new();
        return schema switch
        {
            RecordSchema recordSchema => avroClassGenerator.Generate(@namespace, recordSchema),
            EnumSchema enumSchema => avroEnumGenerator.Generate(@namespace, enumSchema),
            _ => throw new NotImplementedException($"A generator for the specified shema type {schema?.GetType()} is not implemented yet.")
        };
    }
}
