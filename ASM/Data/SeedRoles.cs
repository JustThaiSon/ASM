using ASM.Entities;
using ASM.Entities.Enum;
using ASM.Entities.UserRoles;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ASM.Data
{
	public class SeedRoles
	{
		public static async void Seed(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				try
				{
					var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
					if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
						await roleManager.CreateAsync(new AppRole { Name = UserRoles.Admin, Decription = "Administrator Role" });
					if (!await roleManager.RoleExistsAsync(UserRoles.User))
						await roleManager.CreateAsync(new AppRole { Name = UserRoles.User, Decription = "User Role" });

					// User
					var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
					string adminEmail = "hoangthaisonqs@gmail.com";
					var admin = await userManager.FindByEmailAsync(adminEmail);
					if (admin == null)
					{
						var newAdmin = new AppUser()
						{
							FullName = "Hoang Thai Son",
							UserName = "ThaiSon",
							Email = adminEmail,
							EmailConfirmed = true,
							Gender = GenderMenu.Khác,
							Dob = new DateTime(1998, 1, 1) ,
							Address = "Bắc Giang"

						};
						var result = await userManager.CreateAsync(newAdmin, "ThaiSon98.");
						if (!result.Succeeded)
						{
							throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
						}
						await userManager.AddToRoleAsync(newAdmin, UserRoles.Admin);
					}

					// User
					string userEmail = "ThaiSon98@gmail.com";
					var user = await userManager.FindByEmailAsync(userEmail);
					if (user == null)
					{
						var newUser = new AppUser()
						{
							FullName = "Hoang Thai Son",
							UserName = "ThaiSon98",
							Email = userEmail,
							EmailConfirmed = true,
							Gender = GenderMenu.Nam,
							Dob = new DateTime(1998, 1, 1),
							Address = "Bắc Giang"
						};
						var result = await userManager.CreateAsync(newUser, "ThaiSon98.");
						if (!result.Succeeded)
						{
							throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
						}
						await userManager.AddToRoleAsync(newUser, UserRoles.User);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"An error occurred: {ex.Message}");
					if (ex.InnerException != null)
					{
						Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
					}
				}
			}
		}
	}
}
