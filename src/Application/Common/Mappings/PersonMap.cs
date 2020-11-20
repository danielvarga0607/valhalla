using AutoMapper;

using System;

using Valhalla.Application.Persons.Queries.GetPersons;
using Valhalla.Domain.Entities;

namespace Valhalla.Application.Common.Mappings
{
    public class PersonMap : Profile
    {
        public PersonMap()
        {
            CreateMap<PersonDto, Person>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id == default ? Guid.NewGuid() : src.Id));

            CreateMap<Person, PersonDto>();
        }
    }
}