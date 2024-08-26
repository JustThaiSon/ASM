using ASM.Entities.Enum;
using Microsoft.AspNetCore.Identity;

namespace ASM.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public GenderMenu Gender { get; set; }
        public DateTime Dob {  get; set; }
        public List<Order>? Order { get; set; }
        public List<CartItem>? CartItem { get; set; }

    }
}
