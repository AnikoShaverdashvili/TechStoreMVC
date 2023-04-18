using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using TechStoreMVC.Data.Enum;
using TechStoreMVC.Models;

namespace TechStoreMVC.ViewModel
{
    public class EditLaptopViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
        public Address Address { get; set; }
        public int AddressId { get; set; }
        [DisplayName("Laptop Category")]
        public LaptopCategory LaptopCategory { get; set; }
        public string Processor { get; set; }
        public string GraphicsCard { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
        [DisplayName("Operation System")]
        public string OperatingSystem { get; set; }
        [DisplayName("Display Size")]
        public double DisplaySize { get; set; }
        public double Price { get; set; }
    }
}
