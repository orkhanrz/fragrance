using System.Security.Claims;
using fragrance.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace fragrance.Controllers
{
    public class AuthController : Controller
    {
        YourDbContext db = new YourDbContext();

        public async void AuthUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", Convert.ToString(user.UserId)),
                new Claim(ClaimTypes.Email, user.UserEmail),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Surname", user.UserSurname),
                new Claim("Gender", Convert.ToString(user.UserGender)!),
                new Claim(ClaimTypes.Role, user.UserRole)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [Authorize]
        public IActionResult Account()
        {
            Claim Id = User.FindFirst("Id")!;

            if (Id != null)
            {
                int UserId = int.Parse(Id.Value);
                User user = db.Users.SingleOrDefault(u => u.UserId == UserId)!;
                ViewBag.User = user;
                return View();
            } else {
                return RedirectToAction("Login", "Auth");
            };
        }

        [HttpPost]
        public async Task<IActionResult> Login(string UserEmail, string UserPassword)
        {
            User user = db.Users.SingleOrDefault(u => u.UserEmail == UserEmail && u.UserRole == "customer")!;

            if (user != null && BCrypt.Net.BCrypt.Verify(UserPassword, user.UserPassword))
            {
                AuthUser(user);
                return RedirectToAction("Index", "Home");
            } else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public IActionResult Register(string UserName, string UserSurname, string UserEmail, string UserPassword)
        {
            User user = db.Users.SingleOrDefault(u => u.UserEmail == UserEmail)!;

            if (user != null)
            {
                return RedirectToAction("Login");
            };

            string HashedPassword = BCrypt.Net.BCrypt.HashPassword(UserPassword);


            User u = new User();
            u.UserName = UserName;
            u.UserSurname = UserSurname;
            u.UserEmail = UserEmail;
            u.UserPassword = HashedPassword;

            db.Users.Add(u);
            db.SaveChanges();

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAccount(string UserName, string UserSurname, string UserPassword, string NewPassword, string UserGender)
        {
            Claim Id = User.FindFirst("Id")!;

            if (Id != null)
            {
                int UserId = int.Parse(Id.Value);
                User user = db.Users.SingleOrDefault(u => u.UserId == UserId)!;
                user.UserName = !string.IsNullOrEmpty(UserName) ? UserName : user.UserName;
                user.UserSurname = !string.IsNullOrEmpty(UserSurname) ? UserSurname : user.UserSurname;

                if (short.TryParse(UserGender, out short gender))
                {
                    user.UserGender = gender;
                }

                if (UserPassword != null && BCrypt.Net.BCrypt.Verify(UserPassword, user.UserPassword))
                {
                    user.UserPassword = BCrypt.Net.BCrypt.HashPassword(NewPassword);
                }

                db.SaveChanges();
                AuthUser(user);

                return RedirectToAction("Account", "Auth");
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            };
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}

