﻿//HintName: RecordSchemaPrimitiveTypeNullEntity.g.cs
// <auto-generated>
// This code was generated by: 'Devantler.DataProduct.Generator.IncrementalGenerators.EntitiesGenerator'.
// Any changes made to this file will be overwritten.
using Devantler.DataProduct.Features.Schemas;
namespace Devantler.DataProduct.Features.DataStore.Entities;
/// <summary>
/// An entity class for the RecordSchemaPrimitiveTypeNull record.
/// </summary>
public class RecordSchemaPrimitiveTypeNullEntity : IEntity<Guid>
{
    /// <summary>
    /// The unique identifier for this entity.
    /// </summary>
    public Guid Id { get; set; }
    #nullable enable
    /// <summary>
    /// The NullField property.
    /// </summary>
    public string? NullField { get; set; }
    #nullable disable
}