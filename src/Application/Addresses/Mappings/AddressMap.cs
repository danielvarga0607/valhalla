using System;
using AutoMapper;
using JetBrains.Annotations;
using Valhalla.Application.Addresses.Queries.GetAddresses;
using Valhalla.Domain.Entities;

namespace Valhalla.Application.Addresses.Mappings
{
    [UsedImplicitly]
    public class AddressMap : Profile
    {
        public AddressMap()
        {
            CreateMap<AddressDto, Address>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id == default ? Guid.NewGuid() : src.Id))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode));

            CreateMap<Address, AddressDto>();
        }
    }
}