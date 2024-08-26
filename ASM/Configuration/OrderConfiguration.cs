using ASM.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(o => o.OrderId); 

            builder.Property(o => o.OrderId).UseIdentityColumn(); ; 

            builder.Property(o => o.CreatedDate).IsRequired(); 

            builder.HasOne(o => o.AppUser) 
                   .WithMany(c => c.Order) 
                   .HasForeignKey(o => o.UserId);
        }
    }
}
