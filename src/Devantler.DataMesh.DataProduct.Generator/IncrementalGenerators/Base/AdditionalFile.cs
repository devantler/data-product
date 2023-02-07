using Microsoft.CodeAnalysis.Text;

namespace Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators.Base;

/// <summary>
/// A model for additional files added to the source generator.
/// </summary>
public class AdditionalFile
{
    /// <summary>
    /// Creates a new additional file.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="filePath"></param>
    /// <param name="fileDirectoryPath"></param>
    /// <param name="contents"></param>
    public AdditionalFile(string fileName, string filePath, string fileDirectoryPath, SourceText? contents)
    {
        FileName = fileName;
        FilePath = filePath;
        FileDirectoryPath = fileDirectoryPath;
        Contents = contents;
    }
    /// <summary>
    /// The name of the file (without the file extension).
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
