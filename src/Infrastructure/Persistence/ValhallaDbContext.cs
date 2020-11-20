using Microsoft.EntityFrameworkCore;

using Valhalla.Application.Common.Interfaces;
using Valhalla.Domain.Entities;

namespace Valhalla.Infrastructure.Persistence
{
    public class ValhallaDbContext : DbContext, IValhallaDbContext
    {
        public ValhallaDbContext(DbContextOptions<ValhallaDbContext> options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ValhallaDbContext).Assembly);
        }
    }
}