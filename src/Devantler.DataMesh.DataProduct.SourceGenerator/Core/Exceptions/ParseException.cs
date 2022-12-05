using System;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Core.Exceptions;

public class ParseException : Exception
{
    public ParseException()
    {
    }

    public ParseException(string message) : base(message)
    {
    }

    public ParseException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
