using ASM.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM.Configuration
{
    public class NhaSanXuatConfiguration : IEntityTypeConfiguration<NhaSanXuat>
    {
        public void Configure(EntityTypeBuilder<NhaSanXuat> builder)
        {
            builder.ToTable("NhaSanXuat");

            builder.HasKey(nsx => nsx.NhaSanXuatId); 

            builder.Property(nsx => nsx.NhaSanXuatId).UseIdentityColumn(); ;

            builder.Property(nsx => nsx.TenNhaSanXuat).IsRequired()
                   .HasMaxLength(200); ; ; 
        }
    }
}
