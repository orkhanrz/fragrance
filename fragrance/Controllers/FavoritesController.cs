using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace fragrance.Controllers
{
    public class FavoritesController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}

