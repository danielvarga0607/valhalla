using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Valhalla.Application.Common.Exceptions;
using Valhalla.Application.Common.Interfaces;
using Valhalla.Domain.Entities;

namespace Valhalla.Application.Entities.Commands.DeleteEntity
{
    public class DeleteEntityCommand<TEntity> : IRequest<bool> where TEntity : EntityBase
    {
        public Guid Id { get; set; }
    }

    public class DeleteEntityCommandHandler<TEntity> : IRequestHandler<DeleteEntityCommand<TEntity>, bool>
        where TEntity : EntityBase
    {
        private readonly IValhallaDbContext _valhallaDbContext;

        public DeleteEntityCommandHandler(
            IValhallaDbContext valhallaDbContext)
        {
            _valhallaDbContext = valhallaDbContext;
        }

        public async Task<bool> Handle(DeleteEntityCommand<TEntity> request, CancellationToken cancellationToken)
        {
            var entity = await _valhallaDbContext.Set<TEntity>().FindAsync(request.Id).ConfigureAwait(false);

            if (entity is null)
            {
                throw new NotFoundException(nameof(TEntity), request.Id);
            }

            _valhallaDbContext.Entry(entity).State = EntityState.Deleted;
            await _valhallaDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return true;
        }
    }
}