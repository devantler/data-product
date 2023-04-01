using Microsoft.CodeAnalysis.Text;

namespace Devantler.DataMesh.DataProduct.Generator.Models;

/// <summary>
/// A model for additional files added to the source generator.
/// </summary>
public class AdditionalFile
{
    /// <summary>
    /// The name of the file.
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// The path to the file.
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// The path to the directory containing the file.
    /// </summary>
    public string FileDirectoryPath { get; set; } = string.Empty;

    /// <summary>
    /// The contents of the file.
    /// </summary>
    public SourceText? Contents { get; set; }
}
