using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Valhalla.Domain.Entities;

namespace Valhalla.Infrastructure.Persistence.Configurations
{
    public class PersonTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder
                .Property(entity => entity.Id)
                .ValueGeneratedOnAdd();
        }
    }
}