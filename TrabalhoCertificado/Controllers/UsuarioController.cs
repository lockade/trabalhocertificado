using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TrabalhoCertificado.Data;
using TrabalhoCertificado.Models;

namespace TrabalhoCertificado.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class UsuarioController : Controller
    {

        private readonly DataContext context;

        public UsuarioController(DataContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult Cadastro()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario usuario)
        {

            if (ModelState.IsValid)
            {
                ViewBag.Confirmacao = true;//inserir no banco
            }
            else
            {
                ViewBag.Erro = "Campo(s) inválido(s)";
            }

            
            return View();

        }
        public ActionResult Logoff()
        {
             
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
        public ActionResult perfil()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Usuario login)
        {

            if (login.Email != null && login.Senha != null)
            {
                login.Email = login.Email.Trim();
                login.Senha = login.Senha.Trim();

                //PRECISA CRIPTOGRAFAR SENHA

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
