using ASM.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM.Configuration
{
    public class ThanhPhanConfiguration : IEntityTypeConfiguration<ThanhPhan>
    {
        public void Configure(EntityTypeBuilder<ThanhPhan> builder)
        {
            builder.ToTable("ThanhPhan"); 
            builder.HasKey(tp => tp.ThanhPhanId); 

            builder.Property(tp => tp.ThanhPhanId)
                  .UseIdentityColumn();

            builder.Property(tp => tp.ThongTinThanhPhan) 
                   .HasMaxLength(500); 

            builder.HasOne(tp => tp.Product) 
                   .WithMany(p => p.ThanhPhan) 
                   .HasForeignKey(tp => tp.ProductId);
        }
    }
}
