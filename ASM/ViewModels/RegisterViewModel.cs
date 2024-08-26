using ASM.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace ASM.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		public string? Name { get; set; }	
		[Required]
		public string? UserName { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		public string? Email { get; set; }
		[Required]
		public GenderMenu Gender { get; set; }
		[DataType(DataType.MultilineText)]
		public string? Address { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string? Password { get; set; }
		[Compare("Password", ErrorMessage = "Password don't match")]
		[Display(Name = "Comfirm Password")]
		[DataType(DataType.Password)]
		public string? ConfirmPassword { get; set; }

	}
}
