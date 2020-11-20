using Microsoft.AspNetCore.Mvc;

using Valhalla.Application.Addresses.Queries.GetAddresses;
using Valhalla.Domain.Entities;
using Valhalla.Web.Contracts.V1;

namespace Valhalla.Web.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Addresses)]
    public class AddressController : CrudController<AddressDto, Address>
    {
    }
}