using AutoMapper;
using Devantler.DataMesh.DataProduct.Core.Entities;
using Devantler.DataMesh.DataProduct.Core.Models;

namespace Devantler.DataMesh.DataProduct.Core.Mapping;

public class StudentProfile : Profile
{
    protected StudentProfile()
    {
        CreateMap<StudentEntity, Student>().ReverseMap();
    }
}
