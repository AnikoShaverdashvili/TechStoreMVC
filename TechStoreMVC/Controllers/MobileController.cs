using Microsoft.AspNetCore.Mvc;
using TechStoreMVC.Interfaces;
using TechStoreMVC.Models;

namespace TechStoreMVC.Controllers
{
    public class MobileController : Controller
    {
        private readonly IMobileRepository _mobileRepository;

        public MobileController(IMobileRepository mobileRepository)
        {
            _mobileRepository = mobileRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Mobile> mobiles = await _mobileRepository.GetAllMobile();
            return View(mobiles);
        }

        public async Task<IActionResult> Details(int id)
        {
            Mobile mobile = await _mobileRepository.GetByIdAsyncNoTracking(id);
            return View(mobile);
        }

        //get 

        public IActionResult Create() 
        {
            return View();
        }

        //[HttpPost]

        //public async Task<IActionResult>Create(Mobile mobile)
        //{

        //}

    }
}
