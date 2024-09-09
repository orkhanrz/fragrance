using fragrance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace fragrance.Controllers
{
    public class AdminController : Controller
    {
        YourDbContext db = new YourDbContext();

        [Authorize(AuthenticationSchemes = "AdminScheme")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("AdminScheme");
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string UserEmail, string UserPassword)
        {
            User user = db.Users.SingleOrDefault(u => u.UserEmail == UserEmail && u.UserRole == "admin");

            if (user != null && BCrypt.Net.BCrypt.Verify(UserPassword, user.UserPassword))
            {
                var claims = new List<Claim>
                {
                    new Claim("Id", Convert.ToString(user.UserId)),
                    new Claim(ClaimTypes.Email, user.UserEmail),
                    new Claim(ClaimTypes.Role, user.UserRole)
                };

                    var claimsIdentity = new ClaimsIdentity(claims, "AdminScheme");

                    await HttpContext.SignInAsync("AdminScheme", new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Index", "Admin");
                }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        [Authorize(AuthenticationSchemes = "AdminScheme")]
        public IActionResult Products()
        {
            List<Fragrance> fragrances = db.Fragrances.OrderBy(x => x.FragranceId).ToList();

            ViewBag.Fragrances = fragrances;

            return View();
        }

        [Authorize(AuthenticationSchemes = "AdminScheme")]
        public IActionResult Product(int id)
        {
            Fragrance fragrance = db.Fragrances.SingleOrDefault(x => x.FragranceId == id);

            if (fragrance != null)
            {
                List<FragranceImage> fragranceImages = db.FragranceImages.Where(fi => fi.FragranceId == id).ToList();

                ViewBag.FragranceImages = fragranceImages;
                ViewBag.Fragrance = fragrance;

                return View();
            };

            return RedirectToAction("Products");
        }

        [Authorize(AuthenticationSchemes = "AdminScheme")]
        public IActionResult Orders()
        {
            var orders = db.Orders.Include(o => o.OrderUser).ToList();

            ViewBag.Orders = orders;

            return View();
        }
    }
}

