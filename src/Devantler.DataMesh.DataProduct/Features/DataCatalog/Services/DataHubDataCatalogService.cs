using Chr.Avro.Abstract;
using Chr.Avro.Representation;
using Devantler.Commons.CodeGen.Core;
using Devantler.Commons.CodeGen.Mapping.Avro;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.FeatureFlags;
using Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Extensions;
using Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Helpers;
using Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models;
using Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects;
using Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects.SchemaMetadata;
using Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects.SchemaMetadata.PlatformSchemas;
using Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Entities;
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
        string urn = UrnHelper.CreateUrn("dataProduct", _options.Name);

        var metadata = new Metadata();
        var rootSchema = await _schemaRegistryClient.GetSchemaAsync(_options.SchemaRegistry.Schema.Subject, _options.SchemaRegistry.Schema.Version, stoppingToken);
        AddSchemaMetadata(urn, metadata, rootSchema);
        AddDocumentation(urn, metadata);
        AddLinks(urn, metadata);

        await _dataHubClient.EmitMetadata(metadata, stoppingToken);
    }

    void AddLinks(string urn, Metadata metadata)
    {
        var links = new List<InstitutionalMemoryMetadata>();

        if (_options.FeatureFlags.EnableDashboard)
        {
            links.Add(new InstitutionalMemoryMetadata
            {
                Url = _options.PublicUrl,
                Description = $"Dashboard"
            });
        }

        if (_options.FeatureFlags.EnableApis.Contains(ApiFeatureFlagValues.Rest))
        {
            links.Add(new InstitutionalMemoryMetadata
            {
                Url = _options.PublicUrl + "/swagger",
                Description = $"Rest API"
            });
        }

        if (_options.FeatureFlags.EnableApis.Contains(ApiFeatureFlagValues.GraphQL))
        {
            links.Add(new InstitutionalMemoryMetadata
            {
                Url = _options.PublicUrl + "/graphql",
                Description = $"GraphQL API"
            });
        }

        metadata.Entities.Add(new DatasetEntity
        {
            EntityUrn = urn,
            Aspect = new InstitutionalMemoryAspect
            {
                Elements = links
            }
        });
    }

    void AddDocumentation(string urn, Metadata metadata)
    {
        metadata.Entities.Add(new DatasetEntity
        {
            EntityUrn = urn,
            Aspect = new DatasetPropertiesAspect
            {
                Description = _options.Description + Environment.NewLine + Environment.NewLine + "**Warning!** Metadata for this data product is published automatically. Any changes made to the metadata will likely be overwritten."
            }
        });
    }

    static void AddSchemaMetadata(string urn, Metadata metadata, Chr.Avro.Abstract.Schema rootSchema)
    {
        var schema = rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>().FirstOrDefault()
            ?? throw new InvalidOperationException("The record schema for the data product could not be found.");
        var avroSchemaParser = new AvroSchemaParser();
        var schemaWriter = new JsonSchemaWriter();

        metadata.Entities.Add(new DatasetEntity
        {
            EntityUrn = urn,
            Aspect = new SchemaMetadataAspect
            {
                SchemaName = schema.Name,
                Fields = schema.Fields.Select(f => new SchemaField
                {
                    FieldPath = f.Name,
                    Type = avroSchemaParser.Parse(f.Type, Language.CSharp).ToSchemaFieldType(),
                    Description = f.Documentation ?? string.Empty,
                    NativeDataType = avroSchemaParser.Parse(f.Type, Language.CSharp)
                }).ToList(),
                PlatformSchema = new PlatformDocumentSchema(DocumentSchemaType.KafkaSchema)
                {
                    DocumentSchema = schemaWriter.Write(schema)
                }
            }
        });
    }
}