using Microsoft.AspNetCore.Mvc;

using Valhalla.Application.Addresses.Queries.GetAddresses;
using Valhalla.Domain.Entities;

namespace Valhalla.Web.Controllers
{
    [ApiController]
    [Route("api/v1/addresses")]
    public class AddressController : CrudController<AddressDto, Address>
    {
    }
}