using ASM.Data;
using ASM.Entities;
using ASM.IRepository;
using ASM.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ASM.Repository
{
    public class CartItemRepository : ICartRepository
    {
        private readonly MyDbContext _context;
        public CartItemRepository(MyDbContext context)
        {
            _context = context;
        }
        public void AddToCarts(Guid userId, int productId, int quantity)
        {
            var cartItem = _context.CartItems.SingleOrDefault(c => c.AppUserId == userId && c.ProductId == productId);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    AppUserId = userId,
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }
            _context.SaveChanges();
        }

        public List<CartViewModel> GetCartItems(Guid userId)
        {
            return _context.CartItems
                 .Include(c => c.Product)
                 .Where(c => c.AppUserId == userId)
                 .Select(c => new CartViewModel
                 {
                     ProductId = c.ProductId,
                     ProductName = c.Product.NameProduct,
                     Quantity = c.Quantity,
                     Price = c.Product.Price,
                     ImgUrl = c.Product.ImgUrl 
                 }).ToList();
        }

        public void RemoveFromCart(Guid userId, int productId)
        {
            var cartItem = _context.CartItems.SingleOrDefault(c => c.AppUserId == userId && c.ProductId == productId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
        }

        public void UpdateCart(Guid userId, int productId, int quantity)
        {
            var cartItem = _context.CartItems.SingleOrDefault(c => c.AppUserId == userId && c.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _context.SaveChanges();
            }
        }
    }
}
