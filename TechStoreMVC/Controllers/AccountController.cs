using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechStoreMVC.Data;
using TechStoreMVC.Models;
using TechStoreMVC.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TechStoreMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _serviceProvider = serviceProvider;
        }


        public IActionResult Login()
        {
            var response = new LoginVM();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                //user is found
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    //password correct sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Mobile");
                    }
                }
                //password is incorrect
                TempData["Error"] = "Wrong credentials , please try again";
                return View(loginVM);
            }
            //user not found
            TempData["Error"] = "Wrong credentials , please try again";
            return View(loginVM);
        }


        public IActionResult Register()
        {
            var response = new RegisterVM();
            return View(response);
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email is already in use";
                return View(registerVM);
            }

            var newUser = new AppUser()
            {
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress,
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.ADMIN))
                {
                    var role = new IdentityRole { Name = UserRoles.ADMIN };
                    await roleManager.CreateAsync(role);
                }

                if (!await roleManager.RoleExistsAsync(UserRoles.USER))
                {
                    var role = new IdentityRole { Name = UserRoles.USER };
                    await roleManager.CreateAsync(role);
                }

                var addToRoleResult = await _userManager.AddToRoleAsync(newUser, UserRoles.USER);

                if (addToRoleResult.Succeeded)
                {
                    TempData["Success"] = "Successfully created new account";
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in addToRoleResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(registerVM);
                }
            }

            // If the code reaches this point, it means creating the new user failed, so return a view with an error message
            TempData["Error"] = "Could not create new account";
            return View(registerVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


    }
}
