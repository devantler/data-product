using Devantler.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects;

namespace Devantler.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Entities;

/// <summary>
/// A model that represents the data set entity in the DataHub metadata model.
/// </summary>
public class DatasetEntity : IMetadataEntity
{
    /// <inheritdoc/>
    public required string EntityUrn { get; set; }

    /// <inheritdoc/>
    public string EntityType { get; set; } = "dataset";

    /// <summary>
    /// A schema metadata aspect that describes the schema of the data set.
    /// </summary>
    public required IMetadataAspect Aspect { get; set; }
}