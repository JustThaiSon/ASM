using System.ComponentModel.DataAnnotations;

namespace ASM.ViewModels
{
	public class ThuocCongDungViewModel
	{
		public int Thuoc_CongDungId { get; set; }

		[Required]
		[StringLength(100)]
		public string? TenCongDung { get; set; }
	}
}
