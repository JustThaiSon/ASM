using ASM.Entities;

namespace ASM.ViewModels
{
	public class OrderViewModel
	{
		public int OrderId { get; set; }

		public DateTime CreatedDate { get; set; }

		public Guid UserId { get; set; }

		public int ProductId { get; set; }
		public string Name { get; set; }

		public int Quantity { get; set; }

		public decimal Price { get; set; }
		public AppUser? AppUser { get; set; }
		public List<OrderDetails>? OrderDetails { get; set; }

	}
}
