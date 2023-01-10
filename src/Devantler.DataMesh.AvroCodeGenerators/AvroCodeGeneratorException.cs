namespace Devantler.DataMesh.AvroCodeGenerators;

/// <summary>
/// An exception that is thrown when an error occurs during code generation.
/// </summary>
public class AvroCodeGeneratorException : InvalidOperationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AvroCodeGeneratorException"/> class with no message.
    /// </summary>
    public AvroCodeGeneratorException() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AvroCodeGeneratorException"/> class with a message.
    /// </summary>
    /// <param name="message"></param>
    public AvroCodeGeneratorException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AvroCodeGeneratorException"/> class with a message and an inner exception.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public AvroCodeGeneratorException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
