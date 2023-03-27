using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.Core;
using Devantler.Commons.CodeGen.Mapping.Avro;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Helpers;
using Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models;
using Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects;
using Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Entities;
using Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Schemas;
using Devantler.DataMesh.SchemaRegistryClient;
using Microsoft.Extensions.Options;

namespace Devantler.DataMesh.DataProduct.Features.DataCatalog.Services;

/// <summary>
/// A service that is responsible for interacting with DataHub's data catalog.
/// </summary>
public class DataHubDataCatalogService : BackgroundService
{
    readonly ISchemaRegistryClient _schemaRegistryClient;
    readonly DataHubClient.Client _dataHubClient;
    readonly DataProductOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataHubDataCatalogService"/> class.
    /// </summary>
    /// <param name="scopeFactory"></param>
    public DataHubDataCatalogService(IServiceScopeFactory scopeFactory)
    {
        var scope = scopeFactory.CreateScope();
        _dataHubClient = scope.ServiceProvider.GetRequiredService<DataHubClient.Client>();
        _schemaRegistryClient = scope.ServiceProvider.GetRequiredService<ISchemaRegistryClient>();
        _options = scope.ServiceProvider.GetRequiredService<IOptions<DataProductOptions>>().Value;
    }
    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var rootSchema = await _schemaRegistryClient.GetSchemaAsync(_options.SchemaRegistry.Schema.Subject, _options.SchemaRegistry.Schema.Version, stoppingToken);

        var schema = rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>().FirstOrDefault()
            ?? throw new InvalidOperationException("The record schema for the data product could not be found.");
        var avroSchemaParser = new AvroSchemaParser();

        string urn = UrnHelper.CreateUrn("dataProduct", _options.Name);

        var metadata = new Metadata();
        metadata.Entities.Add(new DatasetEntity
        {
            EntityUrn = urn,
            Aspect = new SchemaMetadataAspect
            {
                SchemaName = schema.Name,
                Fields = schema.Fields.Select(f => new SchemaField
                {
                    FieldPath = f.Name,
                    Type = new SchemaFieldType(SchemaFieldDataType.StringType),
                    Description = f.Documentation ?? string.Empty,
                    NativeDataType = avroSchemaParser.Parse(f.Type, Language.CSharp)
                }).ToList()
            }
        });
        metadata.Entities.Add(new DatasetEntity
        {
            EntityUrn = urn,
            Aspect = new DatasetPropertiesAspect
            {
                Description = _options.Description
            }
        });

        await _dataHubClient.EmitMetadata(metadata, stoppingToken);
    }
}