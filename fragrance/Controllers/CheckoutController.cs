using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using fragrance.Models;
using System.Security.Claims;

namespace fragrance.Controllers
{
    public class CheckoutController : Controller
    {
        YourDbContext db = new YourDbContext();

        [Authorize]
        public IActionResult Index()
        {
            Claim Id = User.FindFirst("Id")!;
            User user = db.Users.SingleOrDefault(u => u.UserId == int.Parse(Id.Value))!;
            ViewBag.User = user;

            return View();
        }
    }
}

