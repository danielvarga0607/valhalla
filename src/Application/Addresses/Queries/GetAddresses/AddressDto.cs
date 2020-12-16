using Valhalla.Application.Common.Interfaces;

namespace Valhalla.Application.Addresses.Queries.GetAddresses
{
    public class AddressDto : IDto
    {
        public int PostalCode { get; set; }
    }
}