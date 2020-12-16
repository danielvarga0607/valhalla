using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Valhalla.Domain.Entities;

namespace Valhalla.Application.Common.Interfaces
{
    public interface IValhallaDbContext
    {
        DbSet<Address> Addresses { get; set; }

        DbSet<Person> Persons { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}