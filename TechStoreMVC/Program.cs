using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechStoreMVC.Data;
using TechStoreMVC.Helpers;
using TechStoreMVC.Interfaces;
using TechStoreMVC.Models;
using TechStoreMVC.Repositories;
using TechStoreMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IMobileRepository, MobileRepository>();
builder.Services.AddScoped<ILaptopRepository, LaptopRepository>();
//image upload

builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
//for identity

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Create admin user and assign "Admin" role
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var userManager = serviceProvider.GetService<UserManager<AppUser>>();
    var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

    if (userManager.FindByEmailAsync("admin@example.com").Result == null)
    {
        var user = new AppUser
        {
            UserName = "admin@example.com",
            Email = "admin@example.com"
        };

        var result = await userManager.CreateAsync(user, "MyAdminPassword1!");

        if (result.Succeeded)
        {
            if (await roleManager.FindByNameAsync("ADMIN") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("ADMIN"));
            }

            await userManager.AddToRoleAsync(user, "ADMIN");
        }
    }
}

app.Run();

