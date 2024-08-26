using ASM.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM.Configuration
{
    public class ThuongHieuConfiguration : IEntityTypeConfiguration<ThuongHieu>
    {
        public void Configure(EntityTypeBuilder<ThuongHieu> builder)
        {
            builder.ToTable("ThuongHieu"); 
            builder.HasKey(th => th.ThuongHieuId); 

            builder.Property(th => th.ThuongHieuId)
              .UseIdentityColumn();

            builder.Property(th => th.TenThuongHieu)
                   .HasMaxLength(200); 
        }
    }
}
