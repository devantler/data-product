﻿//HintName: RecordSchemaPrimitiveTypesController.g.cs
// <auto-generated>
// This code was generated by: 'Devantler.DataProduct.Generator.IncrementalGenerators.RestBulkControllerGenerator'.
// Any changes made to this file will be overwritten.
using AutoMapper;
using Devantler.DataProduct.Features.Schemas;
using Devantler.DataProduct.Features.DataStore.Entities;
using Devantler.DataProduct.Features.DataStore.Services;
namespace Devantler.DataProduct.Features.Apis.Rest.Controllers;
/// <summary>
/// A controller to handle REST API requests for a the <see cref="RecordSchemaPrimitiveTypes" /> schema.
/// </summary>
public class RecordSchemaPrimitiveTypesController : RestBulkController<Guid, RecordSchemaPrimitiveTypes>
{
    /// <summary>
    /// Creates a new instance of <see cref="RecordSchemaPrimitiveTypesController" />
    /// </summary>
    public RecordSchemaPrimitiveTypesController(IDataStoreService<Guid, RecordSchemaPrimitiveTypes> dataStoreService) : base(dataStoreService)
    {
    }
}