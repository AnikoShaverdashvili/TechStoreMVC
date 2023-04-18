using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TechStoreMVC.Data.Enum;
using TechStoreMVC.Models;

namespace TechStoreMVC.ViewModel
{
    public class EditMobileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IFormFile Image { get; set; }
        public string? URL { get; set; }

        public Address Address { get; set; }
        public int AddressId { get; set; }
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
