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

        public async Task <IActionResult> Index()
        {
            IEnumerable<Mobile> mobiles = await _mobileRepository.GetAllMobile();
            return View(mobiles);
        }
    }
}
