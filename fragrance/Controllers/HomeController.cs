using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using fragrance.Models;

namespace fragrance.Controllers;

public class HomeController : Controller
{
    YourDbContext db = new YourDbContext();

    public IActionResult Index()
    {
        List<Fragrance> fragrances = db.Fragrances.OrderByDescending(x => x.CreatedAt).Take(4).ToList();

        ViewBag.fragrances = fragrances;

        return View();
    }
}

