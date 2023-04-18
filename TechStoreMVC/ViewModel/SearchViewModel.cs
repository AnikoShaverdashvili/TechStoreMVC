using TechStoreMVC.Models;

namespace TechStoreMVC.ViewModel
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; }
        public List<Mobile> Mobiles { get; set; }
        public List<Laptop> Laptops { get; set; }
    }

}
