using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TrabalhoCertificado.Models;

namespace TrabalhoCertificado.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult Index(Login login)
        {
            if (ModelState.IsValid)
            {
                if (login.Email == "admin@admin.com" && login.Senha == "admin")
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Sid, "1"),
                        new Claim(ClaimTypes.Name, "Jefferson"),
                        new Claim(ClaimTypes.Role, "A"),
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        IsPersistent = true,
                    };

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Erro = "Usuário e/ou senha inválidos";
            return View();

        }
    }
}
