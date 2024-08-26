using ASM.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ASM.Controllers
{
	public class HomeController : Controller
	{
		private readonly IProductRepository _productRepository;
        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var products = await _productRepository.GetAllProduct(page, pageSize);

            return View(products);
        }
    }
}
