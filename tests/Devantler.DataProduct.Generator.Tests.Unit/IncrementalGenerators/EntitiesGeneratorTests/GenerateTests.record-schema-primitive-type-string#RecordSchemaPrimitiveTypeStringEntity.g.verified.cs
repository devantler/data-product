﻿//HintName: RecordSchemaPrimitiveTypeStringEntity.g.cs
// <auto-generated>
// This code was generated by: 'Devantler.DataProduct.Generator.IncrementalGenerators.EntitiesGenerator'.
// Any changes made to this file will be overwritten.
using Devantler.DataProduct.Features.Schemas;
namespace Devantler.DataProduct.Features.DataStore.Entities;
/// <summary>
/// An entity class for the RecordSchemaPrimitiveTypeString record.
/// </summary>
public class RecordSchemaPrimitiveTypeStringEntity : IEntity<Guid>
{
    /// <summary>
    /// The unique identifier for this entity.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// The StringField property.
    /// </summary>
    public string StringField { get; set; }
}