using ASM.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM.ViewModels
{
    public class ProductViewModels
    {
		[Key]
		public int ProductId { get; set; }
		public string? NameProduct { get; set; }

		public string? ImgUrl { get; set; }

		public string? Description { get; set; }

		public string? CachDung { get; set; }

		public string? LuuY { get; set; }
        public decimal Price { get; set; }
        public string? TenThuongHieu { get; set; }

        [DataType(DataType.Date)]
        public DateTime Hsd { get; set; }

        public string? TenNhaSanXuat { get; set; }

		public string? ThongTinThanhPhan { get; set; }
		public string? CongDungThuoc { get; set; }
		public string? CategoryName { get; set; }

	}
}
