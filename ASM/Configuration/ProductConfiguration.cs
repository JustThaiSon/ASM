using ASM.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product"); 

            builder.HasKey(p => p.ProductId); 

            builder.Property(p => p.ProductId).UseIdentityColumn(); ;

            builder.Property(p => p.NameProduct) 
                   .HasMaxLength(200);

            builder.Property(p => p.ImgUrl);

            builder.Property(p => p.Description) 
                   .HasMaxLength(1000);

            builder.Property(p => p.CachDung) 
                   .HasMaxLength(500);

            builder.Property(p => p.LuuY) 
                   .HasMaxLength(500);

            builder.Property(p => p.ThuongHieuId); 

            builder.Property(p => p.Hsd); 

            builder.Property(p => p.MaNhaSanXuat); 

            builder.HasOne(p => p.NhaSanXuat) 
                   .WithMany(nsx => nsx.Product) 
                   .HasForeignKey(p => p.MaNhaSanXuat); 

            builder.HasOne(p => p.ThuongHieu) 
                   .WithMany()
                   .HasForeignKey(p => p.ThuongHieuId); 
        }
    }
}
