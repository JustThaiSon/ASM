using ASM.Entities;
using ASM.Entities.UserRoles;
using ASM.IRepository;
using ASM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM.Controllers
{
	[Authorize(Roles = UserRoles.Admin)]
	public class CustomerController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public CustomerController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public async Task<IActionResult> GetUser()
		{
			var users = await _userManager.Users.Select(user => new AppUserViewModel
			{
				UserId = user.Id,
				Name = user.FullName,
				Gender = user.Gender,
				Address = user.Address,
				UserName = user.UserName,
				Email = user.Email,
			}).ToListAsync();
			return View(users);
		}
        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest("Invalid user ID");
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return NotFound($"Cannot find user with ID '{userId}'.");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "User successfully deleted.";
                return RedirectToAction(nameof(GetUser));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(nameof(GetUser));
        }
        [HttpGet]
		public async Task<IActionResult> DetailsUser(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null)
			{
				return NotFound();
			}

			var viewModel = new AppUserViewModel
			{
				UserId = user.Id,
				Name = user.FullName,
				Gender = user.Gender,
				Address = user.Address,
				UserName = user.UserName,
				Email = user.Email,
			};

			return View(viewModel);
		}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new AppUserViewModel
            {
                UserId = user.Id,
                Name = user.FullName,
                Gender = user.Gender,
                Address = user.Address,
                UserName = user.UserName,
                Email = user.Email,
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userManager.FindByIdAsync(viewModel.UserId.ToString());
            if (user == null)
            {
                return NotFound();
            }

            user.FullName = viewModel.Name;
            user.Gender = viewModel.Gender;
            user.Address = viewModel.Address;
            user.UserName = viewModel.UserName;
            user.Email = viewModel.Email;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "User successfully updated.";
                return RedirectToAction(nameof(GetUser));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(viewModel);
        }


        [HttpGet]
		public IActionResult ChangePassword()
		{
			return View();
		}
	}
}