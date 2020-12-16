using System;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Valhalla.Application.Common.Exceptions;
using Valhalla.Application.Common.Interfaces;
using Valhalla.Domain.Entities;

namespace Valhalla.Application.Entities.Commands.UpdateEntity
{
    public class UpdateEntityCommand<TDto, TEntity> : IRequest<TEntity>
        where TDto : IDto
        where TEntity : EntityBase
    {
        public Guid Id { get; set; }
        public TDto Dto { get; set; }
    }

    public class UpdateEntityCommandHandler<TDto, TEntity> : IRequestHandler<UpdateEntityCommand<TDto, TEntity>, TEntity>
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

        public async Task<TEntity> Handle(UpdateEntityCommand<TDto, TEntity> request, CancellationToken cancellationToken)
        {
            var entityId = request.Id;
            var entitySet = _valhallaDbContext.Set<TEntity>();
            var entity = await entitySet.FindAsync(entityId);

            if (entity is null)
            {
                throw new NotFoundException(nameof(TEntity), entityId);
            }

            var updatedEntity = _mapper.Map(request.Dto, entity);

            entitySet.Update(updatedEntity);
            await _valhallaDbContext.SaveChangesAsync(cancellationToken);

            return updatedEntity;
        }
    }
}