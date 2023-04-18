using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechStoreMVC.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Address")]
        public string StreetAddress { get; set; }
        public string Country { get; set; }
    }
}
