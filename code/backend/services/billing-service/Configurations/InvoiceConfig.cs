using billing_service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace billing_service.Configurations
{
    public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("invoices");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Number)
                .IsRequired();

            builder.Property(i => i.Status)
                .HasConversion<int>() 
                .IsRequired();

            builder.HasMany(i => i.Items)
                .WithOne()
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
