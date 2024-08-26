using ASM.Entities;
using ASM.IRepository;
using ASM.ViewModels;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASM.Controllers
{
	public class CartController : Controller
	{
		private readonly ICartRepository _cartService;
		private readonly UserManager<AppUser> _userManager;
		private readonly IOrderRepository _orderRepository;
		public CartController(ICartRepository _cartService, UserManager<AppUser> _userManager, IOrderRepository _orderRepository)
		{
			this._cartService = _cartService;
			this._userManager = _userManager;
			this._orderRepository = _orderRepository;
		}
		public IActionResult Index()
		{
			var userId = _userManager.GetUserId(User);
			var cartItems = _cartService.GetCartItems(Guid.Parse(userId));
			return View(cartItems);
		}
		[HttpPost]
		public IActionResult AddToCart(int productId, int quantity)
		{
			var userId = _userManager.GetUserId(User);
			if (userId == null)
			{
				return RedirectToAction("Login", "Account");
			}
			_cartService.AddToCarts(Guid.Parse(userId), productId, quantity);
			return RedirectToAction("Index");
		}
		[HttpPost]
		public IActionResult RemoveFromCart(int productId)
		{
			var userId = _userManager.GetUserId(User);
			_cartService.RemoveFromCart(Guid.Parse(userId), productId);
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult UpdateCart(int productId, int quantity)
		{
			var userId = _userManager.GetUserId(User);
			_cartService.UpdateCart(Guid.Parse(userId), productId, quantity);
			return RedirectToAction("Index");
		}
	}
}
