using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechStoreMVC.ViewModel
{
    public class LoginVM
    {
        [DisplayName("Email Address")]
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]   
        public string Password { get; set; }    
    }
}
