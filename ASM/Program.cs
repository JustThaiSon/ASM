using ASM.Data;
using ASM.Entities;
using ASM.IRepository;
using ASM.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MyDbContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("Db")));
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<INhaSanXuatRepository, NhaSanXuatRepository>();
            builder.Services.AddScoped<IThuongHieuRepository, ThuongHieuRepository>();
            builder.Services.AddScoped<ICongDungRepository, CongDungRepository>();
            builder.Services.AddScoped<ICartRepository, CartItemRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
			builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<MyDbContext>().AddDefaultTokenProviders();
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			   .AddCookie(options =>
			   {
				   options.LoginPath = "/Account/Login";
				   options.AccessDeniedPath = "/Account/AccessDenied";
			   });

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            SeedRoles.Seed(app);
            app.Run();
        }
    }
}
