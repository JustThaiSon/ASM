using ASM.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM.Configuration
{
    public class Product_CategoryConfiguration : IEntityTypeConfiguration<Product_Category>
    {
        public void Configure(EntityTypeBuilder<Product_Category> builder)
        {
            builder.ToTable("Product_Category");
            builder.HasKey(pc => new { pc.CategoryId, pc.ProductId }); 

            builder.HasOne(pc => pc.Category) 
                   .WithMany(c => c.Product_Category) 
                   .HasForeignKey(pc => pc.CategoryId);

            builder.HasOne(pc => pc.Product) 
                   .WithMany(p => p.Product_Category)
                   .HasForeignKey(pc => pc.ProductId);
        }
    }
}
