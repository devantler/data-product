﻿//HintName: RecordSchemaPrimitiveTypeIntEntity.g.cs
// <auto-generated>
// This code was generated by: 'Devantler.DataProduct.Generator.IncrementalGenerators.EntitiesGenerator'.
// Any changes made to this file will be overwritten.
using Devantler.DataProduct.Features.Schemas;
namespace Devantler.DataProduct.Features.DataStore.Entities;
/// <summary>
/// An entity class for the RecordSchemaPrimitiveTypeInt record.
/// </summary>
public class RecordSchemaPrimitiveTypeIntEntity : IEntity<Guid>
{
    /// <summary>
    /// The unique identifier for this entity.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// The IntField property.
    /// </summary>
    public int IntField { get; set; }
}