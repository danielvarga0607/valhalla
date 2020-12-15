﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Valhalla.Application.Addresses.Queries.GetAddresses;
using Valhalla.Application.Entities.Commands.CreateEntity;
using Valhalla.Application.Entities.Commands.DeleteEntity;
using Valhalla.Application.Entities.Commands.UpdateEntity;
using Valhalla.Application.Entities.Queries.GetEntity;
using Valhalla.Domain.Entities;
using Valhalla.Web.Contracts.V1;
using Valhalla.Web.Extensions;

namespace Valhalla.Web.Controllers.V1
{
    [ApiController]
    [Route(ApiRoutes.Addresses)]
    public class AddressController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddressDto dto)
        {
            if (dto.Id != Guid.Empty)
            {
                return BadRequest();
            }

            var entity = await Mediator.Send(new CreateEntityCommand<AddressDto, Address> {Dto = dto});

            return Created(HttpContext.GetLocation(entity.Id), entity);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Read([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new GetEntityQuery<Address> {Dto = new AddressDto {Id = id}}));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AddressDto dto)
        {
            if (dto.Id == Guid.Empty)
            {
                return BadRequest();
            }

            await Mediator.Send(new UpdateEntityCommand<AddressDto> {Dto = dto});

            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteEntityCommand<Address> {Id = id});

            return NoContent();
        }
    }
}