using billing_service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace billing_service.Configurations
{
    public class InvoiceItemConfig : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.ToTable("invoice_items");

            builder.HasKey(ii => ii.Id);

            builder.Property(ii => ii.ProductCode)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(ii => ii.Quantity)
                .IsRequired();
        }
    }
}
