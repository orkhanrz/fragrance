using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using fragrance.Models;
using fragrance.Services;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fragrance.Controllers
{
    public class ApiController : Controller
    {
        YourDbContext db = new YourDbContext();

        [HttpGet("/api/fragrances")]
        public IActionResult GetFragrances(int? limit = null, string brand = "", string line = "", short? gender = null, int? minPrice = null, int? maxPrice = null)
        {
            IQueryable<Fragrance> query = db.Fragrances;

            if (!string.IsNullOrEmpty(brand))
            {
                query = query.Where(x => x.FragranceBrand == brand);
            }

            if (!string.IsNullOrEmpty(line))
            {
                query = query.Where(x => x.FragranceLine == line);
            }

            if (gender.HasValue)
            {
                query = query.Where(x => x.FragranceGender == gender);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(x => x.FragrancePrice >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(x => x.FragrancePrice <= maxPrice.Value);
            }

            if (limit.HasValue)
            {
                query = query.Take(limit.Value);
            }

            List<Fragrance> fragrances = query.OrderByDescending(x => x.CreatedAt).ToList();

            return Json(fragrances);
        }

        [HttpPost("/api/fragrances")]
        public async Task<IActionResult> AddFragrance(Fragrance fr, IFormFile FragranceImage, List<IFormFile> FragranceImages)
        {
            var user = await HttpContext.AuthenticateAsync("AdminScheme");

            if (!user.Succeeded)
            {
                return RedirectToAction("Login", "Admin");
            }

            if (FragranceImage != null && FragranceImage.Length > 0)
            {
                fr.FragranceImage = await FileService.UploadImage(FragranceImage);
            }

            db.Fragrances.Add(fr);
            db.SaveChanges();

            await FileService.UploadFragranceImages(fr.FragranceId, FragranceImages);

            return RedirectToAction("Products", "Admin");
        }



        [HttpGet("/api/fragrances/{fragranceId}")]
        public IActionResult GetSingleFragrance(int fragranceId) {
            Fragrance fragrance = db.Fragrances.SingleOrDefault(f => f.FragranceId == fragranceId)!;

            return Json(fragrance);
        }

        [HttpPost("/api/fragrances/{fragranceId}")]
        public async Task<IActionResult> EditFragrance(int fragranceId, Fragrance fr, IFormFile FragranceImage, List<IFormFile> FragranceImages)
        {
            var user = await HttpContext.AuthenticateAsync("AdminScheme");

            if (!user.Succeeded)
            {
                return RedirectToAction("Login", "Admin");
            };

            Fragrance fragrance = db.Fragrances.SingleOrDefault(f => f.FragranceId == fragranceId)!;

            if (fragrance == null)
            {
                return RedirectToAction("Products", "Admin");
            };

            if (FragranceImage != null && FragranceImage.Length > 0)
            {
                fragrance.FragranceImage = await FileService.UploadImage(FragranceImage);
            };

            db.SaveChanges();

            if (FragranceImages != null)
            {
                List<FragranceImage> oldImages = db.FragranceImages.Where(fi => fi.FragranceId == fragranceId).ToList();
                db.FragranceImages.RemoveRange(oldImages);
                db.SaveChanges();

                FileService.DeleteImages(oldImages);
                await FileService.UploadFragranceImages(fr.FragranceId, FragranceImages);
            };

            fragrance.FragranceBrand = fr.FragranceBrand;
            fragrance.FragranceLine = fr.FragranceLine;
            fragrance.FragrancePrice = fr.FragrancePrice;
            fragrance.FragranceGender = fr.FragranceGender;
            fragrance.FragranceStock = fr.FragranceStock;
            fragrance.FragranceDesc = fr.FragranceDesc;
            fragrance.FragranceLongDesc = fr.FragranceLongDesc;
            db.SaveChanges();

            return RedirectToAction("Products", "Admin");
        }

        [HttpDelete("/api/fragrances/{fragranceId}")]
        public async Task<IActionResult> DeleteFragrance(int fragranceId)
        {
            try
            {
                var user = await HttpContext.AuthenticateAsync("AdminScheme");

                if (!user.Succeeded)
                {
                    return Unauthorized();
                }

                var fragrance = db.Fragrances.SingleOrDefault(f => f.FragranceId == fragranceId);

                if (fragrance == null)
                {
                    return NotFound();
                }

                var fragranceReviews = db.FragranceReviews.Where(r => r.FragranceReviewFragranceId == fragranceId).ToList();
                var fragranceImages = db.FragranceImages.Where(o => o.FragranceId == fragranceId).ToList();
                var favoriteItems = db.FavoriteItems.Where(o => o.FavoriteItemFragranceId == fragranceId).ToList();
                var orderItems = db.OrderItems.Where(o => o.OrderItemFragranceId == fragranceId).ToList();

                db.FragranceReviews.RemoveRange(fragranceReviews);
                db.FragranceImages.RemoveRange(fragranceImages);
                db.FavoriteItems.RemoveRange(favoriteItems);
                db.OrderItems.RemoveRange(orderItems);

                db.Fragrances.Remove(fragrance);

                db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("/api/favorites")]
        public IActionResult GetFavorites()
        {
            Claim userId = User.FindFirst("Id")!;

            if (userId != null)
            {
                var favorites = db.FavoriteItems.Where(f => f.FavoriteItemUserId == int.Parse(userId.Value))
                    .Join(db.Fragrances,
                        fi => fi.FavoriteItemFragranceId,
                        f => f.FragranceId,
                        (fit, ft) => new {
                            ft.FragranceId,
                            ft.FragranceImage,
                            ft.FragranceBrand,
                            ft.FragranceLine,
                            ft.FragrancePrice,
                            ft.FragranceOldPrice
                        }
                    ).
                    ToList();

                return Json(favorites);
            } else
            {
                return Json(new List<object>());
            }

        }

        [HttpPost("/api/favorites/{fragranceId}")]
        public IActionResult AddFavorite(int fragranceId)
        {
            Claim userId = User.FindFirst("Id")!;

            if (userId == null)
            {
                return Json(new { Success = false, Message = "You are not authorized!", WasAdded = false });
            };

            FavoriteItem favoriteItemExists = db.FavoriteItems.SingleOrDefault(f =>
                f.FavoriteItemFragranceId == fragranceId
                && f.FavoriteItemUserId == int.Parse(userId.Value)
            )!;

            if (favoriteItemExists != null)
            {
                db.FavoriteItems.Remove(favoriteItemExists);
                db.SaveChanges();
                return Json(new { Success = true, Message = "Item was removed from favorites!", WasAdded = false });
            } else
            {
                FavoriteItem favoriteItem = new FavoriteItem
                {
                    FavoriteItemFragranceId = fragranceId,
                    FavoriteItemUserId = int.Parse(userId.Value)
                };

                db.FavoriteItems.Add(favoriteItem);
                db.SaveChanges();
                return Json(new { Success = true, Message = "Item was added to favorites!", WasAdded = true });
            }
        }

        [HttpPost("/api/reviews")]
        public IActionResult AddReview([FromBody] FragranceReview fr)
        {
            try
            {
                Claim userId = User.FindFirst("Id")!;

                if (userId != null)
                {
                    fr.FragranceReviewUserId = int.Parse(userId.Value);

                    db.FragranceReviews.Add(fr);
                    db.SaveChanges();

                    return Json(new { Success = true, Message = "Review has been added!", Review = fr });
                   
                } else
                {
                    return Json(new { Success = false, Message = "You are not authorized!" });
                };
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            };
        }

        [HttpPost("/api/checkout")]
        public async Task<IActionResult> Checkout([FromBody] List<CartItemDto> cartItems)
        {
            Claim userId = User.FindFirst("Id")!;

            int totalAmount = cartItems.Sum(item => item.FragrancePrice * item.Count);

            var order = new Order
            {
                OrderUserId = int.Parse(userId.Value),
                OrderTotalPrice = totalAmount,
            };

            db.Orders.Add(order);
            await db.SaveChangesAsync();

            var OrderItems = cartItems.Select(item => new OrderItem
            {
                OrderItemOrderId = order.OrderId,
                OrderItemFragranceId = item.FragranceId,
                OrderItemQuantity = item.Count,
                OrderItemUnitPrice = item.FragrancePrice * item.Count
            }).ToList();

            db.OrderItems.AddRange(OrderItems);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("/api/orders/{id}")]
        public async Task<IActionResult> OrderDetails(int id)
        {
            var user = await HttpContext.AuthenticateAsync("AdminScheme");

            if (!user.Succeeded)
            {
                return Unauthorized();
            }

            Order order = db.Orders
                .Include(o => o.OrderUser)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.OrderItemFragrance)
                .SingleOrDefault(o => o.OrderId == id)!;

            if (order == null)
            {
                return NotFound();
            };

            return Json(order);
        }

        [HttpDelete("/api/orders/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var user = await HttpContext.AuthenticateAsync("AdminScheme");

                if (!user.Succeeded)
                {
                    return Unauthorized();
                }

                Order order = db.Orders
                    .Include(o => o.OrderItems)
                    .SingleOrDefault(o => o.OrderId == id)!;

                if (order == null)
                {
                    return NotFound();
                };

                db.OrderItems.RemoveRange(order.OrderItems);
                db.Orders.Remove(order);
                db.SaveChanges();

                return Ok();
            } catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}

