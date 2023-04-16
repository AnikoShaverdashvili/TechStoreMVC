using Microsoft.AspNetCore.Mvc;
using TechStoreMVC.Interfaces;
using TechStoreMVC.Models;

namespace TechStoreMVC.Controllers
{
    public class LaptopController : Controller
    {
        private readonly ILaptopRepository _laptopRepository;
        public LaptopController(ILaptopRepository laptopRepository)
        {
            _laptopRepository = laptopRepository;
        }

        public async Task <IActionResult> Index()
        {
            IEnumerable <Laptop> laptops= await _laptopRepository.GetLaptop();
            return View(laptops);  
        }


        public async Task<IActionResult>Details(int id)
        {
            Laptop laptop = await _laptopRepository.GetLaptopById(id);
            return View(laptop);
        }
    }
}
