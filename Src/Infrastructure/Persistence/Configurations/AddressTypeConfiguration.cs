using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Valhalla.Domain.Entities;

namespace Valhalla.Infrastructure.Persistence.Configurations
{
    public class AddressTypeConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(entity => entity.Id);
            
            builder
                .Property(entity => entity.Id)
                .ValueGeneratedOnAdd();
        }
    }
}