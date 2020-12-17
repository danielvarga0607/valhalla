using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Valhalla.Application.Addresses.Queries.GetAddresses;
using Valhalla.Application.Common.Exceptions;
using Valhalla.Application.Entities.Commands.CreateEntity;
using Valhalla.Application.Entities.Commands.DeleteEntity;
using Valhalla.Application.Entities.Commands.UpdateEntity;
using Valhalla.Application.Entities.Queries.ReadEntity;
using Valhalla.Domain.Entities;
using Valhalla.Web.Contracts.V1;
using Valhalla.Web.Extensions;

namespace Valhalla.Web.Controllers.V1
{
    [ApiController]
    public class AddressController : ApiControllerBase
    {
        [HttpPost(ApiRoutes.Addresses.Create)]
        public async Task<IActionResult> Create([FromBody] AddressDto dto)
        {
            var entity = await Mediator.Send(new CreateEntityCommand<Address> {Dto = dto});
            var location = HttpContext.GetLocation(entity.Id);
            
            return Created(location, entity);
        }

        [HttpGet(ApiRoutes.Addresses.Get)]
        public async Task<IActionResult> Read([FromRoute] Guid addressId)
        {
            if (addressId == Guid.Empty)
            {
                throw new AppException();
            }
            
            var addressDto = await Mediator.Send(new ReadEntityQuery<Address> {Id = addressId});
            
            return Ok(addressDto);
        }

        [HttpPut(ApiRoutes.Addresses.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid addressId, [FromBody] AddressDto dto)
        {
            if (addressId == Guid.Empty)
            {
                throw new AppException();
            }

            var updatedAddress = await Mediator.Send(new UpdateEntityCommand<AddressDto, Address>
            {
                Id = addressId,
                Dto = dto
            });

            return Ok(updatedAddress);
        }

        [HttpDelete(ApiRoutes.Addresses.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid addressId)
        {
            await Mediator.Send(new DeleteEntityCommand<Address> {Id = addressId});

            return NoContent();
        }
    }
}