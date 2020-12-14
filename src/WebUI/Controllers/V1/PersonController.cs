using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Valhalla.Application.Entities.Commands.CreateEntity;
using Valhalla.Application.Entities.Commands.DeleteEntity;
using Valhalla.Application.Entities.Commands.UpdateEntity;
using Valhalla.Application.Entities.Queries.GetEntity;
using Valhalla.Application.Persons.Queries.GetPersons;
using Valhalla.Domain.Entities;
using Valhalla.Web.Contracts.V1;
using Valhalla.Web.Extensions;

namespace Valhalla.Web.Controllers.V1
{
    [ApiController]
    [Route(ApiRoutes.People)]
    public class PersonController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonDto dto)
        {
            if (dto.Id != Guid.Empty)
            {
                return BadRequest();
            }

            var entityId = await Mediator.Send(new CreateEntityCommand<PersonDto> {Dto = dto});

            return Created(HttpContext.GetLocation(entityId), dto);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Read([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new GetEntityQuery<Person> {Dto = new PersonDto {Id = id}}));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PersonDto dto)
        {
            if (dto.Id == Guid.Empty)
            {
                return BadRequest();
            }

            await Mediator.Send(new UpdateEntityCommand<PersonDto> {Dto = dto});

            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteEntityCommand<Person> {Id = id});

            return NoContent();
        }
    }
}