using ASM.Entities.UserRoles;
using ASM.IRepository;
using ASM.Repository;
using ASM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASM.Controllers
{
	[Authorize(Roles = UserRoles.Admin)]

	public class CategoriesController : Controller
	{
		private readonly ICategoryRepository _categoryRepository;
		public CategoriesController(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;

		}

		public async Task<IActionResult> GetAll(string KeyWord, int page = 1, int pageSize = 4)
		{
            var categories = await _categoryRepository.GetAllCategory(page, pageSize);
            var totalCategories = await _categoryRepository.GetTotalCategories();
			if (!string.IsNullOrEmpty(KeyWord))
			{
				categories =  categories.Where(x => x.NameProduct.StartsWith(KeyWord) || x.CategoryName.StartsWith(KeyWord)).ToList();
			}
			ViewBag.TotalPages = (int)Math.Ceiling(totalCategories / (double)pageSize);
            ViewBag.CurrentPage = page;
			ViewBag.KeyWord = KeyWord;
			return View(categories);
		}

		public async Task<IActionResult> Create()
		{
			ViewBag.Product = await _categoryRepository.GetProduct();

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(CategoryViewModels request)
		{
			if (ModelState.IsValid)
			{
				var Create = await _categoryRepository.CreateCategory(request);
				return RedirectToAction("GetAll");
			}
			return View(request);
		}
		public async Task<IActionResult> Update(int Id)
		{
			var categories = await _categoryRepository.GetCategoryById(Id);
			ViewBag.Product = await _categoryRepository.GetProduct();
			return View(categories);
		}
		[HttpPost]
		public async Task<IActionResult> Update(CategoryViewModels request)
		{
			if (ModelState.IsValid)
			{
				var Create = await _categoryRepository.UpdateCategory(request);
				return RedirectToAction("GetAll");
			}
			return View(request);
		}
		public async Task<IActionResult> Deltails(int Id)
		{
			var categories = await _categoryRepository.GetCategoryById(Id);
			return View(categories);
		}

		public async Task<IActionResult> Delete(int Id)
		{
			bool isDeleted = await _categoryRepository.DeleteCategory(Id);

			if (isDeleted)
			{
				TempData["Message"] = "Category deleted successfully.";
				return RedirectToAction("GetAll");
			}
			else
			{
				TempData["Error"] = "Failed to delete Category.";
				return View();
			};
		}
	}
}
