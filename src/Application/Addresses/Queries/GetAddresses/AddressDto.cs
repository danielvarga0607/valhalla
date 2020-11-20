using System;

using Valhalla.Application.Common.Interfaces;

namespace Valhalla.Application.Addresses.Queries.GetAddresses
{
    public class AddressDto : IDto
    {
        public Guid Id { get; set; }

        public int PostalCode { get; set; }
    }
}