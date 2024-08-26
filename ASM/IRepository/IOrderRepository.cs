using ASM.Entities;
using ASM.ViewModels;

namespace ASM.IRepository
{
	public interface IOrderRepository
	{
		Task<List<OrderViewModel>> GetAllOrders(int page, int pageSize);

		Task<OrderViewModel> GetOrderById(int id);

		Task<bool> CreateOrder(OrderViewModel request);

		Task<bool> UpdateOrder(Order request);

		Task<bool> DeleteOrder(int id);

		Task<List<OrderViewModel>> GetOrderDetails(int orderId);

		Task<int> GetTotalOrders();
	}
}
