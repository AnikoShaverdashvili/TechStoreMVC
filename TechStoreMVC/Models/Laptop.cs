using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TechStoreMVC.Data.Enum;
using System.ComponentModel;

namespace TechStoreMVC.Models
{
    public class Laptop
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public LaptopCategory LaptopCategory { get; set; }
        public string Processor { get; set; }
        public string GraphicsCard { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
        [DisplayName("Operating System")]
        public string OperatingSystem { get; set; }
        public double DisplaySize { get; set; }
        public double Price { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
