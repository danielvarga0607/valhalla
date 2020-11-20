using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using System;
using System.Threading;
using System.Threading.Tasks;

using Valhalla.Application.Common.Extensions;
using Valhalla.Application.Common.Interfaces;
using Valhalla.Domain.Entities;

namespace Valhalla.Application.Entities.Commands.UpdateEntity
{
    public class UpdateEntityCommand<TDto> : IRequest<Guid> where TDto : IDto
    {
        public TDto Dto { get; set; }
    }

    public class UpdateEntityCommandHandler<TDto, TEntity> : IRequestHandler<UpdateEntityCommand<TDto>, Guid>
        where TDto : IDto
        where TEntity : EntityBase
    {
        private readonly IValhallaDbContext _valhallaDbContext;
        private readonly IMapper _mapper;

        public UpdateEntityCommandHandler(
            IValhallaDbContext valhallaDbContext,
            IMapper mapper)
        {
            _valhallaDbContext = valhallaDbContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateEntityCommand<TDto> request, CancellationToken cancellationToken)
        {
            var entityId = request.Dto.Id;
            var entity = await _valhallaDbContext.Set<TEntity>().FindAsync(entityId).ConfigureAwait(false);

            if (entity is null)
            {
                throw new NotFoundException(nameof(TEntity), entityId);
            }

            var updatedEntity = _mapper.Map(request.Dto, entity);

            _valhallaDbContext.Entry(updatedEntity).State = EntityState.Modified;
            await _valhallaDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return updatedEntity.Id;
        }
    }
}