using Microsoft.AspNetCore.Mvc;
using TechStoreMVC.Interfaces;
using TechStoreMVC.Models;
using TechStoreMVC.ViewModel;

namespace TechStoreMVC.Controllers
{
    public class LaptopController : Controller
    {
        private readonly ILaptopRepository _laptopRepository;
        private readonly IPhotoService _photoService;

        public LaptopController(ILaptopRepository laptopRepository, IPhotoService photoService)
        {
            _laptopRepository = laptopRepository;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Laptop> laptops = await _laptopRepository.GetLaptop();
            return View(laptops);
        }


        public async Task<IActionResult> Details(int id)
        {
            Laptop laptop = await _laptopRepository.GetLaptopById(id);
            return View(laptop);
        }

        //get 

        public IActionResult Create(Laptop laptop)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLaptopViewModel laptVm)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(laptVm.Image);
                if (laptVm.OperatingSystem != "Windows" && laptVm.OperatingSystem != "MacOS" && laptVm.OperatingSystem != "Linux")
                {
                    ModelState.AddModelError(nameof(CreateLaptopViewModel.OperatingSystem), "Invalid Operating System. Please select either Windows, MacOS or Linux.");
                    return View(laptVm);
                }
                if (laptVm.Price <= 0)
                {
                    ModelState.AddModelError("", "Price cant be lower than 0");
                    return View(laptVm);
                }

                var laptop = new Laptop
                {
                    Name = laptVm.Name,
                    Description = laptVm.Description,
                    Image = result.Url.ToString(),
                    LaptopCategory = laptVm.LaptopCategory,
                    Processor = laptVm.Processor,
                    GraphicsCard = laptVm.GraphicsCard,
                    RAM = laptVm.RAM,
                    Storage = laptVm.Storage,
                    OperatingSystem = laptVm.OperatingSystem,
                    DisplaySize = laptVm.DisplaySize,
                    Price = laptVm.Price,
                    Address = new Address
                    {
                        Country = laptVm.Address.Country,
                        StreetAddress = laptVm.Address.StreetAddress
                    }
                };
                _laptopRepository.Add(laptop);
                TempData["success"] = "Laptop created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Image upload failed");
            }
            return View(laptVm);
        }



        public async Task<IActionResult>Edit(int id)
        {
            var laptop = await _laptopRepository.GetByIdAsyncNoTracking(id);
            if (laptop == null)
            {
                return View("Error");
            }
            var laptVm = new EditLaptopViewModel
            {
                Name = laptop.Name,
                Description = laptop.Description,
                AddressId = laptop.AddressId,
                Address = laptop.Address,
                URL = laptop.Image,
                LaptopCategory = laptop.LaptopCategory,
                Processor = laptop.Processor,
                Price = laptop.Price,
                Storage = laptop.Storage,
                OperatingSystem = laptop.OperatingSystem,
                DisplaySize = laptop.DisplaySize,
                RAM = laptop.RAM,
                GraphicsCard = laptop.GraphicsCard,

            };
            return View(laptVm);
            
        }

        [HttpPost]

        public async Task<IActionResult>Edit (int id, EditLaptopViewModel laptVm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit laptop");
                return View("Edit", laptVm);
            }
            var userLaptop = await _laptopRepository.GetByIdAsyncNoTracking(id);
            if (userLaptop == null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userLaptop.Image);
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete image");
                    return View(laptVm);
                }

                var photoResult = await _photoService.AddPhotoAsync(laptVm.Image);

                var laptop = new Laptop
                {
                    Id = id,
                    Name = laptVm.Name,
                    Description = laptVm.Description,
                    AddressId = laptVm.AddressId,
                    Address = laptVm.Address,
                    Image = photoResult.Url.ToString(),
                    DisplaySize = laptVm.DisplaySize,
                    Storage = laptVm.Storage,
                    LaptopCategory = laptVm.LaptopCategory,
                    Processor = laptVm.Processor,
                    Price = laptVm.Price,
                    OperatingSystem = laptVm.OperatingSystem,
                    RAM = laptVm.RAM,
                    GraphicsCard = laptVm.GraphicsCard,
                };
                _laptopRepository.Update(laptop);
                return RedirectToAction("Index");

            }

            else
            {
                return View(laptVm);
            }
        }

    }
}
