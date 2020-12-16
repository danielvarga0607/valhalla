using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Valhalla.Application.Common.Exceptions;
using Valhalla.Application.Entities.Commands.CreateEntity;
using Valhalla.Application.Entities.Commands.DeleteEntity;
using Valhalla.Application.Entities.Commands.UpdateEntity;
using Valhalla.Application.Entities.Queries.ReadEntity;
using Valhalla.Application.Persons.Queries.GetPersons;
using Valhalla.Domain.Entities;
using Valhalla.Web.Contracts.V1;
using Valhalla.Web.Extensions;

namespace Valhalla.Web.Controllers.V1
{
    [ApiController]
    public class PersonController : ApiControllerBase
    {
        [HttpPost(ApiRoutes.People.Create)]
        public async Task<IActionResult> Create([FromBody] PersonDto dto)
        {
            if (dto.Id != Guid.Empty)
            {
                throw new AppException();
            }

            var entity = await Mediator.Send(new CreateEntityCommand<Person> {Dto = dto});

            return Created(HttpContext.GetLocation(entity.Id), entity);
        }

        [HttpGet(ApiRoutes.People.Get)]
        public async Task<IActionResult> Read([FromRoute] Guid personId)
        {
            return Ok(await Mediator.Send(new ReadEntityQuery<Person> {Dto = new PersonDto {Id = personId}}));
        }

        [HttpPut(ApiRoutes.People.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid personId, [FromBody] PersonDto dto)
        {
            if (dto.Id == Guid.Empty)
            {
                throw new AppException();
            }
            
            var updatedPerson= await Mediator.Send(new UpdateEntityCommand<PersonDto, Person> {Dto = dto});

            return Ok(updatedPerson);
        }

        [HttpDelete(ApiRoutes.People.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid personId)
        {
            await Mediator.Send(new DeleteEntityCommand<Person> {Id = personId});

            return NoContent();
        }
    }
}