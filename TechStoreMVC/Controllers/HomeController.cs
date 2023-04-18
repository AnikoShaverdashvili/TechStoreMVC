using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechStoreMVC.Data;
using TechStoreMVC.Models;
using TechStoreMVC.ViewModel;
using System.Diagnostics;
using System.Linq;

namespace TechStoreMVC.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            var mobiles = _dbContext.Mobiles
                .Where(m => m.Name.Contains(searchTerm))
                .ToList();

            var laptops = _dbContext.Laptops
                .Where(l => l.Name.Contains(searchTerm))
                .ToList();

            var viewModel = new SearchViewModel
            {
                SearchTerm = searchTerm,
                Mobiles = mobiles,
                Laptops = laptops
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Search(SearchViewModel searchViewModel)
        {
            return RedirectToAction("Search", new { searchTerm = searchViewModel.SearchTerm });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
