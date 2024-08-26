using ASM.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(c => c.CustomerId);

            builder.Property(c => c.CustomerId).UseIdentityColumn(); ; 

            builder.Property(c => c.UserName).IsRequired()
                   .HasMaxLength(200); ;

            builder.Property(c => c.PassWord).IsRequired()
                   .HasMaxLength(200); ;

            builder.Property(c => c.FullName).IsRequired()
                   .HasMaxLength(200); ;

            builder.Property(c => c.Email).IsRequired()
                   .HasMaxLength(200); ;

            builder.Property(c => c.Address).IsRequired()
                   .HasMaxLength(200); ;

            builder.Property(c => c.Gender).IsRequired()
                   .HasMaxLength(200); ;

            builder.Property(c => c.DateOfBirth).IsRequired()
                   .HasMaxLength(200); ;

        }
    }
}
