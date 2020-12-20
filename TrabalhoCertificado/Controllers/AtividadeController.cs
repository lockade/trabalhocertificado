using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrabalhoCertificado.Data;

namespace TrabalhoCertificado.Controllers
{
    public class AtividadeController : Controller
    {
        private readonly DataContext context;

        public AtividadeController(DataContext _context)
        {
            context = _context;
        }

        public ActionResult Index()
        {
            var livros = context.TBAtividades.ToList();
            return View(livros);
        }
        public ActionResult Create()
        {
            return View();
        }
    }
}
