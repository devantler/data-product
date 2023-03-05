﻿//HintName: Query.g.cs
// <auto-generated>
// This code was generated by: 'Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators.GraphQLQueryGenerator'.
// Any changes made to this file will be overwritten.
using Devantler.DataMesh.DataProduct.DataStore.Services;
using Devantler.DataMesh.DataProduct.Models;
namespace Devantler.DataMesh.DataProduct.Apis.GraphQL;
public partial class Query
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IQueryable<RecordSchemaPrimitiveTypes>> GetRecordSchemaPrimitiveTypes([Service] IDataStoreService<RecordSchemaPrimitiveTypes> dataStoreService, CancellationToken cancellationToken)
        => await dataStoreService.GetAllAsQueryableAsync(cancellationToken);

}