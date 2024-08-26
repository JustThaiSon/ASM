using ASM.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM.Configuration
{
    public class CongDungConfiguration : IEntityTypeConfiguration<CongDung>
    {
        public void Configure(EntityTypeBuilder<CongDung> builder)
        {
            builder.ToTable("CongDung"); 

            builder.HasKey(cd => cd.CongDungId); 

            builder.Property(cd => cd.CongDungId).UseIdentityColumn();

            builder.Property(cd => cd.CongDungThuoc).IsRequired()
                   .HasMaxLength(200); ;


        }
    }
}
