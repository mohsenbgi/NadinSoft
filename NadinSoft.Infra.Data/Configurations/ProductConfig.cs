using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NadinSoft.Domain.Entities;

namespace NadinSoft.Infra.Data.Configurations
{
    internal sealed class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(200);
            
            builder.Property(x => x.ManufacturePhone).HasMaxLength(11);
            
            builder.Property(x => x.ManufactureEmail).HasMaxLength(200);

            builder.HasIndex(x => x.ProduceDate).IsUnique();

            builder.HasIndex(x => x.ManufactureEmail).IsUnique();
        }
    }
}
