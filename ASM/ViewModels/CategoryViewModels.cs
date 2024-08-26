using ASM.Entities;
using System.ComponentModel.DataAnnotations;

namespace ASM.ViewModels
{
	public class CategoryViewModels
	{
		[Key]
		public int CategoryId { get; set; }

		public string? NameProduct { get; set; }

		public string? CategoryName { get; set; }

	}
}
