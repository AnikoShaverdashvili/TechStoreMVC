using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TechStoreMVC.Data.Enum;
using TechStoreMVC.Models;

namespace TechStoreMVC.ViewModel
{
    public class CreateMobileViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Please select an image")]
        [Display(Name = "Image")]
        public IFormFile Image { get; set; }
        public Address Address { get; set; }
        public MobileCategory MobileCategory { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        [DisplayName("Screen Size")]
        public double ScreenSize { get; set; }
        [DisplayName("Operating System")]
        public string OperatingSystem { get; set; }
        public double Price { get; set; }
    }
}
