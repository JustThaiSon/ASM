using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASM.Entities
{
	public class OrderDetails
	{
		public int OrderId { get; set; }

		public int ProductId { get; set; }

		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public Product? Product { get; set; }
		public Order? Order { get; set; }

	}
}
