using AutoMapper;
using Devantler.DataMesh.DataProduct.DataStore.Relational.Entities;
using Devantler.DataMesh.DataProduct.Models;

namespace Devantler.DataMesh.DataProduct.Mapping.Profiles;

//TODO: Generate with AutoMapperProfileGenerator
/// <summary>
/// AutoMapper profile for mapping between Student and StudentEntity.
/// </summary>
public class StudentMapperProfile : Profile
{
    /// <summary>
    /// Creates a new instance of the <see cref="StudentMapperProfile"/> class.
    /// </summary>
    public StudentMapperProfile()
        => CreateMap<Student, StudentEntity>().ReverseMap();
}
