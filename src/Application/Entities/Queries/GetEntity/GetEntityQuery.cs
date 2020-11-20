using AutoMapper;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

using Valhalla.Application.Common.Extensions;
using Valhalla.Application.Common.Interfaces;
using Valhalla.Domain.Entities;

namespace Valhalla.Application.Entities.Queries.GetEntity
{
    public class GetEntityQuery<TDto> : IRequest<TDto> where TDto : IDto
    {
        public Guid Id { get; set; }
    }

    public class GetEntityQueryHandler<TDto, TEntity> : IRequestHandler<GetEntityQuery<TDto>, TDto>
        where TDto : IDto
        where TEntity : EntityBase
    {
        private readonly IValhallaDbContext _valhallaDbContext;
        private readonly IMapper _mapper;

        public GetEntityQueryHandler(
            IValhallaDbContext valhallaDbContext,
            IMapper mapper)
        {
            _valhallaDbContext = valhallaDbContext;
            _mapper = mapper;
        }

        public async Task<TDto> Handle(GetEntityQuery<TDto> request, CancellationToken cancellationToken)
        {
            var entity = await _valhallaDbContext.Set<TEntity>().FindAsync(request.Id).ConfigureAwait(false);

            if (entity is null)
            {
                throw new NotFoundException(nameof(TEntity), request.Id);
            }

            return _mapper.Map<TDto>(entity);
        }
    }
}