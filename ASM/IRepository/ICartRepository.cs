using ASM.Entities;
using ASM.ViewModels;

namespace ASM.IRepository
{
    public interface ICartRepository
    {
        void AddToCarts(Guid userId, int productId, int quantity);
        void RemoveFromCart(Guid userId, int productId);
        void UpdateCart(Guid userId, int productId, int quantity);
        List<CartViewModel> GetCartItems(Guid userId);
    }
}
