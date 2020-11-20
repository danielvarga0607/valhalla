using AutoMapper;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

using Valhalla.Application.Common.Interfaces;
using Valhalla.Domain.Entities;

namespace Valhalla.Application.Entities.Commands.CreateEntity
{
    public class CreateEntityCommand<TEntity> : IRequest<Guid>
    {
        public IDto Dto { get; set; }
    }

    public class CreateEntityCommandHandler<TEntity> : IRequestHandler<CreateEntityCommand<TEntity>, Guid>
        where TEntity : EntityBase
    {
        private readonly IMapper _mapper;
        private readonly IValhallaDbContext _valhallaDbContext;

        public CreateEntityCommandHandler(
            IMapper mapper,
            IValhallaDbContext valhallaDbContext)
        {
            _mapper = mapper;
            _valhallaDbContext = valhallaDbContext;
        }

        public async Task<Guid> Handle(CreateEntityCommand<TEntity> request, CancellationToken cancellationToken)
        {
            TEntity entity = _mapper.Map<TEntity>(request.Dto);

            await _valhallaDbContext.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);
            await _valhallaDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return entity.Id;
        }
    }
}