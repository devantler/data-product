using System;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Exceptions;

public class ParseException : Exception
{
    public ParseException(string message) : base(message)
    {
    }
}
