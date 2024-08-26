using ASM.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM.Configuration
{
    public class OrderDetailsConfiguation : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("OrderDetails");

            builder.HasKey(od => new { od.OrderId, od.ProductId }); 


            builder.Property(od => od.Quantity).IsRequired(); 

            builder.Property(od => od.Price).IsRequired(); 

            builder.HasOne(od => od.Product) 
                   .WithMany(p => p.OrderDetails) 
                   .HasForeignKey(od => od.ProductId); 

            builder.HasOne(od => od.Order)
                   .WithMany(o => o.OrderDetails) 
                   .HasForeignKey(od => od.OrderId); 
        }
    }
}
