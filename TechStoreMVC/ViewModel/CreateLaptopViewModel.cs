using System.ComponentModel.DataAnnotations;
using TechStoreMVC.Data.Enum;
using TechStoreMVC.Models;

namespace TechStoreMVC.ViewModel
{
    public class CreateLaptopViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select an image")]
        [Display(Name = "Image")]
        public IFormFile Image { get; set; }

        [Required]
        public LaptopCategory LaptopCategory { get; set; }

        [Required]
        public string Processor { get; set; }

        [Required]
        [Display(Name = "Graphics Card")]
        public string GraphicsCard { get; set; }

        [Required]
        public int RAM { get; set; }

        [Required]
        public int Storage { get; set; }

        [Required]
        [Display(Name = "Operating System")]
        public string OperatingSystem { get; set; }

        [Required]
        [Display(Name = "Display Size")]
        public double DisplaySize { get; set; }

        [Required]
        public double Price { get; set; }

        public Address Address { get; set; }
    }

}
