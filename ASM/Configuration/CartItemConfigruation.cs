using ASM.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ASM.Configuration
{
    public class CartItemConfigruation : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItem");
            builder.HasKey(t => t.Id);
            builder
            .HasOne(ci => ci.Product)
           .WithMany(p => p.CartItem)
           .HasForeignKey(ci => ci.ProductId);

            builder
                .HasOne(ci => ci.AppUser)
                .WithMany(u => u.CartItem)
                .HasForeignKey(ci => ci.AppUserId);
        }
    }
}
