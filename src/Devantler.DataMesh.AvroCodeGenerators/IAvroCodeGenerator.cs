namespace Devantler.DataMesh.AvroCodeGenerators;

/// <summary>
/// An interface for an Avro Code Generator.
/// </summary>
public interface IAvroCodeGenerator<T> where T : Schema
{
    /// <summary>
    /// Generates code from a <see cref="Schema"/>.
    /// </summary>
    /// <param name="namespace"></param>
    /// <param name="schema"></param>
    /// <returns>The generated code as raw text.</returns>
    public string Generate(string @namespace, T schema);
}
