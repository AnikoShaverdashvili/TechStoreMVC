using Microsoft.AspNetCore.Identity;

namespace TechStoreMVC.Models
{
    public class AppUser:IdentityUser
    {
        public ICollection<Mobile> Mobiles { get; set; }
        public ICollection<Laptop> Laptops { get; set; }
    }
}
