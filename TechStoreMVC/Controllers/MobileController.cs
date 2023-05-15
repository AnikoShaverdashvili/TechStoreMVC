using Microsoft.AspNetCore.Mvc;
using TechStoreMVC.Interfaces;
using TechStoreMVC.Models;
using TechStoreMVC.ViewModel;

namespace TechStoreMVC.Controllers
{
    public class MobileController : Controller
    {
        private readonly IMobileRepository _mobileRepository;
        private readonly IPhotoService _photoService;

        public MobileController(IMobileRepository mobileRepository, IPhotoService photoService)
        {
            _mobileRepository = mobileRepository;
            _photoService = photoService;
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


        [HttpPost]
        public async Task<IActionResult> Create(CreateMobileViewModel mobVm)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(mobVm.Image);
                if (mobVm.Price <= 0)
                {
                    ModelState.AddModelError("", "Price cant be lower than thero");
                    return View(mobVm);
                }
                var mobile = new Mobile
                {
                    Name = mobVm.Name,
                    Description = mobVm.Description,
                    Image = result.Url.ToString(),
                    Brand = mobVm.Brand,
                    Model = mobVm.Model,
                    ScreenSize = mobVm.ScreenSize,
                    Price = mobVm.Price,
                    MobileCategory = mobVm.MobileCategory,
                    OperatingSystem = mobVm.OperatingSystem,
                    Address = new Address
                    {
                        Country = mobVm.Address.Country,
                        StreetAddress = mobVm.Address.StreetAddress
                    }
                };
                _mobileRepository.Add(mobile);
                TempData["success"] = "Mobile created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Image upload failed");
                TempData["error"] = "Mobile creation failed";
            }
            return View(mobVm);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var mobile = await _mobileRepository.GetByIdAsyncNoTracking(id);
            if (mobile == null)
            {
                return View("Error");
            }
            var mobVm = new EditMobileViewModel
            {
                Name = mobile.Name,
                Description = mobile.Description,
                AddressId = mobile.AddressId,
                Address = mobile.Address,
                URL = mobile.Image,
                Brand = mobile.Brand,
                Model = mobile.Model,
                OperatingSystem = mobile.OperatingSystem,
                ScreenSize = mobile.ScreenSize,
                MobileCategory = mobile.MobileCategory,
                Price = mobile.Price,
            };
            return View(mobVm);

        }


        [HttpPost]
        public async Task<ActionResult> Edit(int id, EditMobileViewModel mobVm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit mobile");
                return View("Edit", mobVm);
            }
            var userMobile = await _mobileRepository.GetByIdAsyncNoTracking(id);
            if (userMobile != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userMobile.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Couldnt delete photo");
                    return View(mobVm);
                }
                var photoResult = await _photoService.AddPhotoAsync(mobVm.Image);

                var mobile = new Mobile
                {
                    Id = id,
                    Name = mobVm.Name,
                    Description = mobVm.Description,
                    AddressId = mobVm.AddressId,
                    Address = mobVm.Address,
                    Image = photoResult.Url.ToString(),
                    Brand = mobVm.Brand,
                    Model = mobVm.Model,
                    OperatingSystem = mobVm.OperatingSystem,
                    ScreenSize = mobVm.ScreenSize,
                    MobileCategory = mobVm.MobileCategory,
                    Price = mobVm.Price,
                };
                _mobileRepository.Update(mobile);
                TempData["success"] = "Mobile edited successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(mobVm);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var mobile = await _mobileRepository.GetMobileById(id);
            if (mobile == null)
            {
                return View("Error");
            }
            return View(mobile);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteMobile(Mobile mobile)
        {
            if (mobile == null)
            {
                return View("Error");
            }
            _mobileRepository.Delete(mobile);
            return RedirectToAction("Index");
        }


    }
}
