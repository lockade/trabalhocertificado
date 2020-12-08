using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrabalhoCertificado.Data;
using TrabalhoCertificado.Models;

namespace TrabalhoCertificado.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext context;

        public HomeController(ILogger<HomeController> logger, DataContext context_ )
        {
            _logger = logger;
            context = context_;
        }


        public IActionResult Index()
        {

            if (User.IsInRole("administrador"))
            {
                return RedirectToAction("Index", "Admin");
            }

            return View();
        }

        [Authorize]
        public IActionResult Perfil()
        {
            var sid = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());

            Usuario u = context.TBUsuario.FirstOrDefault(x => x.ID == sid);
            return View(u);
        }

        [Authorize]
        public IActionResult AlterarSenhaUser()
        {
            var sid = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());

            Usuario u = context.TBUsuario.FirstOrDefault(x => x.ID == sid);
            return View(u);
        }

        [Authorize(Roles = "usuario")]
        public IActionResult RemoverConta()
        {
            var sid = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());

            Usuario u = context.TBUsuario.FirstOrDefault(x => x.ID == sid);
            return View(u);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
