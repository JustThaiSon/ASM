using Microsoft.AspNetCore.Identity;

namespace ASM.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Decription { get; set; }
    }
}
