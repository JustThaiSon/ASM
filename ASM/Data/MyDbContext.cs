using ASM.Configuration;
using ASM.Entities;
using Microsoft.EntityFrameworkCore;
using ASM.ViewModels;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ASM.Data
{
    public class MyDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CongDungConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new NhaSanXuatConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailsConfiguation());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new Product_CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ThanhPhanConfiguration());
            modelBuilder.ApplyConfiguration(new Thuoc_Cong_Dung_Configuration());
            modelBuilder.ApplyConfiguration(new ThuongHieuConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemConfigruation());
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);
            //modelBuilder.Seed();
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CongDung> CongDungs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<NhaSanXuat> NhaSanXuats { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product_Category> Product_Categorys { get; set; }
        public DbSet<ThanhPhan> ThanhPhans { get; set; }
        public DbSet<Thuoc_CongDung> Thuoc_CongDungs { get; set; }
        public DbSet<ThuongHieu> ThuongHieus { get; set; }
        public DbSet<ASM.ViewModels.AppUserViewModel> AppUserViewModel { get; set; } = default!;
        //public DbSet<ASM.ViewModels.ProductViewModels> ProductViewModels { get; set; } = default!;
        //public DbSet<ASM.ViewModels.CreateProductViewModel> CreateProductViewModel { get; set; } = default!;
        //public DbSet<ASM.ViewModels.CategoryViewModels> CategoryViewModels { get; set; } = default!;
        //public DbSet<ASM.ViewModels.AppUserViewModel> AppUserViewModel { get; set; } = default!;
    }
}