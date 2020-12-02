using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrabalhoCertificado.Data;
using TrabalhoCertificado.Models;

namespace TrabalhoCertificado.Controllers
{
    [Authorize(Roles = "administrador")]
    public class AdminController : Controller
    {

        private readonly DataContext context;

        public AdminController(DataContext _context)
        {
            context = _context;
        }


        public IActionResult Index()
        {
            List<Usuario> u = context.TBUsuario.OrderBy(c => c.ativado).ThenBy(c => c.nome).ToList();
            return View(u);
        }
    }
}
