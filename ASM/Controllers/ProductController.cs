using ASM.Entities;
using ASM.Entities.UserRoles;
using ASM.IRepository;
using ASM.Repository;
using ASM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace ASM.Controllers
{
	[Authorize(Roles = UserRoles.Admin)]
	public class ProductController : Controller
	{
		private readonly IProductRepository _productRepository;
		private readonly INhaSanXuatRepository _nhaSanXuatRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly IThuongHieuRepository _thuongHieuRepository;
		private readonly ICongDungRepository _congDungRepository;

		public ProductController(IProductRepository productRepository, INhaSanXuatRepository nhaSanXuatRepository, 
			ICategoryRepository categoryRepository, IThuongHieuRepository thuongHieuRepository,
			ICongDungRepository congDungRepository)
		{
			_productRepository = productRepository;
			_nhaSanXuatRepository = nhaSanXuatRepository;
			_categoryRepository = categoryRepository;
			_thuongHieuRepository = thuongHieuRepository;
			_congDungRepository = congDungRepository;
		}
		public async Task<IActionResult> GetAllProduct(string Categories, string KeyWord, int page = 1, int pageSize = 3)
		{
			var product = await _productRepository.GetAllProduct(page, pageSize);
			var TotalProduct = await _productRepository.GetTotalProduct();
			if (!string.IsNullOrEmpty(KeyWord))
			{
				product = product.Where(x => x.NameProduct.StartsWith(KeyWord)).ToList();
			}
			if (!string.IsNullOrEmpty(Categories))
			{
				product = product.Where(x => x.CategoryName.StartsWith(Categories)).ToList();
			}
			ViewBag.TotalPages = (int)Math.Ceiling(TotalProduct / (double)pageSize);
			ViewBag.CurrentPage = page;
			ViewBag.KeyWord = KeyWord;
			ViewBag.Categories = await _productRepository.GetCategories();
			return View(product);
		}
		public async Task<IActionResult> Create()
		{
			ViewBag.CongDung = await _productRepository.GetCongDung();
			ViewBag.ThuongHieu = await _productRepository.GetThuongHieus();
			ViewBag.NhaSanXuat = await _productRepository.GetNhaSanXuats();
			ViewBag.ThanhPhan = await _productRepository.GetThanhPhan();
			ViewBag.Categories = await _productRepository.GetCategories();
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(CreateProductViewModel request)
		{
			if (ModelState.IsValid)
			{
				var productId = await _productRepository.CreateProduct(request);
				return RedirectToAction("GetAllProduct");
			}

			ViewBag.CongDung = await _productRepository.GetCongDung();
			ViewBag.ThuongHieu = await _productRepository.GetThuongHieus();
			ViewBag.NhaSanXuat = await _productRepository.GetNhaSanXuats();  
			ViewBag.ThanhPhan = await _productRepository.GetThanhPhan();
			ViewBag.Categories = await _productRepository.GetCategories();

			return View(request);
		}
		public async Task<IActionResult> Delete(int Id)
		{
			bool isDeleted = await _productRepository.DeleteProduct(Id);

			if (isDeleted)
			{
				TempData["Message"] = "Product deleted successfully.";
				return RedirectToAction("GetAllProduct");
			}
			else
			{
				TempData["Error"] = "Failed to delete product.";
				return View();
			};
		}
		public async Task<IActionResult> Details(int Id)
		{
			var exist = await _productRepository.GetProductById(Id);
			return View(exist);
		}
		public async Task<IActionResult> Update(int id)
		{
			var product = await _productRepository.GetProductById(id);
			if (product == null)
			{
				return NotFound();
			}
			ViewBag.CongDung = await _productRepository.GetCongDung();
			ViewBag.ThuongHieu = await _productRepository.GetThuongHieus();
			ViewBag.NhaSanXuat = await _productRepository.GetNhaSanXuats();
			ViewBag.ThanhPhan = await _productRepository.GetThanhPhan();
			ViewBag.Categories = await _productRepository.GetCategories();

			return View(product);
		}
		[HttpPost]
		public async Task<IActionResult> Update(CreateProductViewModel request)
		{
			if (ModelState.IsValid)
			{
				var result = await _productRepository.UpdateProduct(request);
				if (result)
				{
					return RedirectToAction("GetAllProduct");
				}
				ModelState.AddModelError(string.Empty, "An error occurred while updating the product.");
			}


			ViewBag.CongDung = await _productRepository.GetCongDung();
			ViewBag.ThuongHieu = await _productRepository.GetThuongHieus();
			ViewBag.NhaSanXuat = await _productRepository.GetNhaSanXuats();
			ViewBag.ThanhPhan = await _productRepository.GetThanhPhan();
			ViewBag.Categories = await _productRepository.GetCategories();
			return View(request);
		}
		// NhaSanXuat
		public async Task<IActionResult> GetAllNhaSanXuat(string KeyWord, int page = 1, int pageSize = 4)
		{
			var NhaSanXuat = await _nhaSanXuatRepository.GetAllNhaSanXuat(page, pageSize);
			var TotalNhaSanXuat = await _nhaSanXuatRepository.GetTotalNhaSanXuat();
			if (!string.IsNullOrEmpty(KeyWord))
			{
				NhaSanXuat = NhaSanXuat.Where(x => x.TenNhaSanXuat.StartsWith(KeyWord)).ToList();
			}
			ViewBag.TotalPages = (int)Math.Ceiling(TotalNhaSanXuat / (double)pageSize);
			ViewBag.CurrentPage = page;
			ViewBag.KeyWord = KeyWord;
			return View(NhaSanXuat);
		}
		public IActionResult CreateNhaSanXuat()
		{
			return View();
		}
		[HttpPost]
		public async Task <IActionResult> CreateNhaSanXuat(NhaSanXuat request)
		{
            if (ModelState.IsValid)
            {
				var Create = await _nhaSanXuatRepository.CreateNhaSanXuat(request);
				return RedirectToAction("GetAllNhaSanXuat");
			}
            return View();
		}
		public async Task< IActionResult> UpdateNhaSanXuat(int Id)
		{
			var Exists = await _nhaSanXuatRepository.GetNhaSanXuatById(Id);
			return View(Exists);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateNhaSanXuat(NhaSanXuat request)
		{
			if (ModelState.IsValid)
			{
				var Create = await _nhaSanXuatRepository.UpdateNhaSanXuat(request);
				return RedirectToAction("GetAllNhaSanXuat");
			}
			return View();
		}
		public async Task<IActionResult> DeleteNhaSanXuat(int Id)
		{
			bool isDeleted = await _nhaSanXuatRepository.DeleteNhaSanXuat(Id);

			if (isDeleted)
			{
				TempData["Message"] = "NhaSanXuat deleted successfully.";
				return RedirectToAction("GetAllNhaSanXuat");
			}
			else
			{
				TempData["Error"] = "Failed to delete NhaSanXuat.";
				return View();
			};
		}

		// Thuong Hieu
		public async Task<IActionResult> GetAllThuongHieu(string KeyWord, int page = 1, int pageSize = 4)
		{
			var ThuongHieu = await _thuongHieuRepository.GetAllThuongHieu(page, pageSize);
			var TotalThuonghieu = await _thuongHieuRepository.GetTotalThuongHieu();
			if (!string.IsNullOrEmpty(KeyWord))
			{
				ThuongHieu = ThuongHieu.Where(x => x.TenThuongHieu.StartsWith(KeyWord)).ToList();
			}
			ViewBag.TotalPages = (int)Math.Ceiling(TotalThuonghieu / (double)pageSize);
			ViewBag.CurrentPage = page;
			ViewBag.KeyWord = KeyWord;
			return View(ThuongHieu);
		}
		public IActionResult CreateThuongHieu()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateThuongHieu(ThuongHieu request)
		{
			if (ModelState.IsValid)
			{
				var Create = await _thuongHieuRepository.CreateThuongHieu(request);
				return RedirectToAction("GetAllThuongHieu");
			}
			return View();
		}
		public async Task<IActionResult> UpdateThuongHieu(int Id)
		{
			var Exists = await _thuongHieuRepository.GetThuongHieutById(Id);
			return View(Exists);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateThuongHieu(ThuongHieu request)
		{
			if (ModelState.IsValid)
			{
				var Create = await _thuongHieuRepository.UpdateThuongHieu(request);
				return RedirectToAction("GetAllThuongHieu");
			}
			return View();
		}
		public async Task<IActionResult> DeleteThuongHieu(int Id)
		{
			bool isDeleted = await _thuongHieuRepository.DeleteThuongHieu(Id);

			if (isDeleted)
			{
				TempData["Message"] = "NhaSanXuat deleted successfully.";
				return RedirectToAction("GetAllThuongHieu");
			}
			else
			{
				TempData["Error"] = "Failed to delete NhaSanXuat.";
				return View();
			};
		}

		//Cong Dung 

		public async Task<IActionResult> GetAllCongDung(string KeyWord, int page = 1, int pageSize = 4)
		{
			var CongDung = await _congDungRepository.GetAllCongDung(page, pageSize);
			var TotalCongDung = await _congDungRepository.GetTotalCongDung();
			if (!string.IsNullOrEmpty(KeyWord))
			{
				CongDung = CongDung.Where(x => x.CongDungThuoc.StartsWith(KeyWord)).ToList();
			}
			ViewBag.TotalPages = (int)Math.Ceiling(TotalCongDung / (double)pageSize);
			ViewBag.CurrentPage = page;
			ViewBag.KeyWord = KeyWord;
			return View(CongDung);
		}
		public IActionResult CreateCongDung()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateCongDung(CongDung request)
		{
			if (ModelState.IsValid)
			{
				var Create = await _congDungRepository.CreateCongDung(request);
				return RedirectToAction("GetAllCongDung");
			}
			return View();
		}
		public async Task<IActionResult> UpdateCongDung(int Id)
		{
			var Exists = await _congDungRepository.GetCongDungtById(Id);
			return View(Exists);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateCongDung(CongDung request)
		{
			if (ModelState.IsValid)
			{
				var Create = await _congDungRepository.UpdateCongDung(request);
				return RedirectToAction("GetAllCongDung");
			}
			return View();
		}
		public async Task<IActionResult> DeleteCongDung(int Id)
		{
			bool isDeleted = await _congDungRepository.DeleteCongDung(Id);

			if (isDeleted)
			{
				TempData["Message"] = "CongDung deleted successfully.";
				return RedirectToAction("GetAllCongDung");
			}
			else
			{
				TempData["Error"] = "Failed to delete CongDung.";
				return View();
			};
		}
	}
}
