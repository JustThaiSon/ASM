using ASM.Entities;
using ASM.Entities.UserRoles;
using ASM.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASM.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<AppUser> _SignInManager;
		private readonly UserManager<AppUser> _UserManager;
		public AccountController(SignInManager<AppUser> SignInManager, UserManager<AppUser> UserManager)
		{
			_SignInManager = SignInManager;
			_UserManager = UserManager;
		}
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
				if (result.Succeeded)
				{
					var user = await _UserManager.FindByNameAsync(model.UserName);
					if (user != null)
					{
						var roles = await _UserManager.GetRolesAsync(user);
						if (roles.Contains(UserRoles.Admin))
						{
							return RedirectToAction("GetAllProduct", "Product");
						}
					}
					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", "Invalid login attempt");
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var usersExist = _UserManager.Users.ToList();
				AppUser user = new AppUser()
				{
					FullName = model.Name,
					Email = model.Email,
					UserName = model.UserName,
					Address = model.Address,
					Gender = model.Gender
				};
				var result = await _UserManager.CreateAsync(user, model.Password!);
				if (result.Succeeded)
				{
					if (usersExist.Count < 1)
					{
						await _UserManager.AddToRoleAsync(user, UserRoles.Admin);
					}
					else
					{
						await _UserManager.AddToRoleAsync(user, UserRoles.User);
					}
					await _SignInManager.SignInAsync(user, false);
					return RedirectToAction("Index", "Home");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _SignInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
