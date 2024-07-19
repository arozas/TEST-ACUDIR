using Acudir.Test.Application.Responses;
using Acudir.Test.Core.Entities;
using AutoMapper;

namespace Acudir.Test.Application.Mappers;

public class PersonMappingProfile : Profile
{
    public PersonMappingProfile()
    {
        CreateMap<Person, PersonResponse>().ReverseMap();
    }
}