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
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public MobileCategory MobileCategory { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public double ScreenSize { get; set; }
        public string OperatingSystem { get; set; }
        public double Price { get; set; }

    }
}
