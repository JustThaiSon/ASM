using System.ComponentModel.DataAnnotations;

namespace ASM.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string? UserName { get; set; }

        public string? PassWord { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }
        public string? Gender { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? DateOfBirth { get; set; }
        public List<Order>? Order { get; set; }

    }
}
