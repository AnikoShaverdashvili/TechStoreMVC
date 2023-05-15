using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using TechStoreMVC.Data.Enum;

namespace TechStoreMVC.Models
{
    public class Mobile
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Mobile Name")]
        public string Name { get; set; }

        [Display(Name = "Mobile Description")]
        public string Description { get; set; }

        [Display(Name = "Mobile Image")]
        public string Image { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }

        [Display(Name = "Category")]
        public MobileCategory MobileCategory { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }

        [Display(Name = "Screen Size")]
        public double ScreenSize { get; set; }

        [Display(Name = "Operating System")]
        public string OperatingSystem { get; set; }

        [Display(Name = "Price")]
        public double Price { get; set; }

        public string? AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser? AppUser { get; set; }

    }
}
