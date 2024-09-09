using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using fragrance.Models;

namespace fragrance.Controllers
{
    public class ProductsController : Controller
    {
        YourDbContext db = new YourDbContext();

        public IActionResult Index()
        {
            return View();
        }

        [Route("products/{id}")]
        public IActionResult Product(int id)
        {
            Fragrance fragrance = db.Fragrances.SingleOrDefault(x => x.FragranceId == id);

            if (fragrance == null)
            {
                return RedirectToAction("Index", "Products");
            }

            List<FragranceImage> fragranceImages = db.FragranceImages.Where(fi => fi.FragranceId == fragrance.FragranceId).ToList();

            List<Fragrance> relatedFragrances = db.Fragrances
            .Where(y => y.FragranceId != id)
            .OrderByDescending(y => y.FragranceBrand == fragrance.FragranceBrand)
            .ThenByDescending(y => y.FragranceGender == fragrance.FragranceGender)
            .Take(4)
            .ToList();

            var reviews = db.FragranceReviews
                .Where(r => r.FragranceReviewFragranceId == fragrance.FragranceId)
                .Join(db.Users,
                    r => r.FragranceReviewUserId,
                    u => u.UserId,
                    (rt, ut) => new {
                        rt.FragranceReviewTitle,
                        rt.FragranceReviewText,
                        rt.FragranceReviewDate,
                        rt.FragranceReviewRating,
                        ut.UserName,
                        ut.UserSurname
                    }
                 )
                .ToList();

            double fragranceReviewAverage = db.FragranceReviews.Where(fr => fr.FragranceReviewFragranceId == fragrance.FragranceId).Any()
                ? db.FragranceReviews
                .Where(fr => fr.FragranceReviewFragranceId == fragrance.FragranceId)
                .Average(x => (double)(x.FragranceReviewRating ?? 0))
                : 0;

            ViewBag.Fragrance = fragrance;
            ViewBag.FragranceImages = fragranceImages;
            ViewBag.Related = relatedFragrances;
            ViewBag.Reviews = reviews;
            ViewBag.ReviewsAverage = fragranceReviewAverage;
            ViewBag.ReviewsCount = reviews.Count;

            return View();
        }
    }
}

