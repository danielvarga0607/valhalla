using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Valhalla.Application.Common.Exceptions;
using Valhalla.Application.Common.Interfaces;
using Valhalla.Domain.Entities;

namespace Valhalla.Application.Entities.Commands.DeleteEntity
{
    public class DeleteEntityCommand<TEntity> : IRequest<int> where TEntity : EntityBase
    {
        public Guid Id { get; set; }
    }

    public class DeleteEntityCommandHandler<TEntity> : IRequestHandler<DeleteEntityCommand<TEntity>, int>
        where TEntity : EntityBase
    {
        private readonly IValhallaDbContext _valhallaDbContext;

        public DeleteEntityCommandHandler(
            IValhallaDbContext valhallaDbContext)
        {
            _valhallaDbContext = valhallaDbContext;
        }

        public async Task<int> Handle(DeleteEntityCommand<TEntity> request, CancellationToken cancellationToken)
        {
            var entitySet = _valhallaDbContext.Set<TEntity>();
            var entity = await entitySet.FindAsync(request.Id);

            if (entity is null)
            {
                throw new NotFoundException(nameof(TEntity), request.Id);
            }

            entitySet.Remove(entity);
            return await _valhallaDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}