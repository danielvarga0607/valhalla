using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Valhalla.Application.Common.Interfaces;
using Valhalla.Domain.Entities;

namespace Valhalla.Application.Entities.Commands.CreateEntity
{
    public class CreateEntityCommand<TDto> : IRequest<Guid> where TDto : IDto
    {
        public TDto Dto { get; set; }
    }

    public class CreateEntityCommandHandler<TDto, TEntity> : IRequestHandler<CreateEntityCommand<TDto>, Guid>
        where TDto : IDto
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

        public async Task<Guid> Handle(CreateEntityCommand<TDto> request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(request.Dto);

            await _valhallaDbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            await _valhallaDbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}