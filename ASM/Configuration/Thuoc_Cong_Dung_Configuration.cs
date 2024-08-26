using ASM.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM.Configuration
{
    public class Thuoc_Cong_Dung_Configuration : IEntityTypeConfiguration<Thuoc_CongDung>
    {
        public void Configure(EntityTypeBuilder<Thuoc_CongDung> builder)
        {
            builder.ToTable("Thuoc_CongDung"); 

            builder.HasKey(tc => new { tc.ProductId, tc.CongDungId });

            builder.HasOne(tc => tc.Product) 
                   .WithMany(p => p.Thuoc_CongDung) 
                   .HasForeignKey(tc => tc.ProductId); 

            builder.HasOne(tc => tc.CongDung) 
                   .WithMany(c => c.Thuoc_CongDung) 
                   .HasForeignKey(tc => tc.CongDungId); 
        }
    }
}
