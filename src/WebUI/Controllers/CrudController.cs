using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

using Valhalla.Application.Common.Interfaces;
using Valhalla.Application.Entities.Commands.CreateEntity;
using Valhalla.Application.Entities.Commands.DeleteEntity;
using Valhalla.Application.Entities.Commands.UpdateEntity;
using Valhalla.Application.Entities.Queries.GetEntity;
using Valhalla.Domain.Entities;

namespace Valhalla.Web.Controllers
{
    [ApiController]
    public abstract class CrudController<TDto, TEntity> : ApiController
        where TDto : IDto
        where TEntity : EntityBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(TDto dto)
        {
            return Ok(await Mediator.Send(new CreateEntityCommand<TEntity> { Dto = dto }).ConfigureAwait(false));
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Read(Guid id)
        {
            return Ok(await Mediator.Send(new GetEntityQuery<TDto> { Id = id }).ConfigureAwait(false));
        }

        [HttpPatch]
        public async Task<IActionResult> Update(TDto dto)
        {
            return Ok(await Mediator.Send(new UpdateEntityCommand<TDto> { Dto = dto }).ConfigureAwait(false));
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteEntityCommand<TEntity> { Id = id }).ConfigureAwait(false));
        }
    }
}