using AutoMapper;
using DAL.Model;

namespace RedisDistributedCaching.Dto;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Add as many of these lines as you need to map your objects
        CreateMap<Person, PersonDto>();
        CreateMap<PersonDto, Person>();

    }
}

