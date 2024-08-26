using ASM.Data;
using ASM.Entities;
using ASM.IRepository;
using ASM.ViewModels;

namespace ASM.Repository
{
	public class OrderRepository : IOrderRepository
	{
		private readonly MyDbContext _context;
		public OrderRepository(MyDbContext _context)
		{
			this._context = _context;
		}
		public async Task<bool> CreateOrder(OrderViewModel request)
		{
			try
			{
				var order = new Order
				{
					CreatedDate = DateTime.UtcNow, 
					UserId = request.UserId,
					OrderDetails = new List<OrderDetails>()
				};

				foreach (var detail in request.OrderDetails)
				{
					order.OrderDetails.Add(new OrderDetails
					{
						ProductId = detail.ProductId,
						Quantity = detail.Quantity,
						Price = detail.Price
					});
				}

				await _context.Orders.AddAsync(order);
				await _context.SaveChangesAsync();

				return true;
			}
			catch (Exception ex)
			{

				return false;
			}
		}



		public Task<bool> DeleteOrder(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<OrderViewModel>> GetAllOrders(int page, int pageSize)
		{
			throw new NotImplementedException();
		}

		public Task<OrderViewModel> GetOrderById(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<OrderViewModel>> GetOrderDetails(int orderId)
		{
			throw new NotImplementedException();
		}

		public Task<int> GetTotalOrders()
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateOrder(Order request)
		{
			throw new NotImplementedException();
		}
	}
}
