using ASM.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category"); 

            builder.HasKey(c => c.CategoryId); 
            builder.Property(c => c.CategoryId)  
                   .UseIdentityColumn();

            builder.Property(c => c.CategoryName) 
                   .IsRequired() 
                   .HasMaxLength(200); 
        }
    }
}
