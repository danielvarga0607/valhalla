using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Valhalla.Application.Common.Exceptions;
using Valhalla.Application.Common.Interfaces;
using Valhalla.Domain.Entities;

namespace Valhalla.Application.Entities.Queries.GetEntity
{
    public class GetEntityQuery<TEntity> : IRequest<IDto> where TEntity : EntityBase
    {
        public IDto Dto { get; set; }
    }

    public class GetEntityQueryHandler<TDto, TEntity> : IRequestHandler<GetEntityQuery<TEntity>, IDto>
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

        public async Task<IDto> Handle(GetEntityQuery<TEntity> request, CancellationToken cancellationToken)
        {
            var id = request.Dto.Id;
            var entity = await _valhallaDbContext.Set<TEntity>().FindAsync(id);

            if (entity is null)
            {
                throw new NotFoundException(nameof(TEntity), id);
            }

            return _mapper.Map<TDto>(entity);
        }
    }
}