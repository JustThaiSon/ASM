using ASM.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace ASM.ViewModels
{
	public class AppUserViewModel
	{
		[Key]
		public Guid UserId { get; set; }
		public string? Name { get; set; }
		public string? UserName { get; set; }

		public string? Email { get; set; }
		[Required]
		public GenderMenu Gender { get; set; }
		public string? Address { get; set; }
	}
}
