using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using stock_service.Models;

namespace stock_service.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();


            entity.Property(p => p.Code)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(50);

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.Quantity)
                .IsRequired();


        }
    }
}
