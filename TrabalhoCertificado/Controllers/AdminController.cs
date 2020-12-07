using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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



        [HttpGet]
        public IActionResult Index(string s)
        {

            if(s != null)
            {
                List<Usuario> u = context.TBUsuario.OrderBy(c => c.ativado).ThenBy(c => c.nome).Where(x => x.nome.Contains(s) || x.Email.Contains(s)).ToList();
                return View(u);
            }
            else
            {
                List<Usuario> u = context.TBUsuario.OrderBy(c => c.ativado).ThenBy(c => c.nome).ToList();
                return View(u);
            }
            
        }

        [HttpGet]
        public IActionResult UsuariosAtivos(string s)
        {
            ViewBag.Acao = "UsuariosAtivos";
            if (s != null)
            {
                List<Usuario> u = context.TBUsuario.OrderBy(c => c.ativado).ThenBy(c => c.nome).Where(x => x.nome.Contains(s) || x.Email.Contains(s) && x.ativado == true).ToList();
                return View("Index", u);
            }
            else
            {
                List<Usuario> u = context.TBUsuario.OrderBy(c => c.ativado).ThenBy(c => c.nome).Where(x => x.ativado == true).ToList();
                return View("Index", u);
            }
        }

        [HttpGet]
        public IActionResult UsuariosDesativados(string s)
        {
            ViewBag.Acao = "UsuariosDesativados";
            if (s != null)
            {
                List<Usuario> u = context.TBUsuario.OrderBy(c => c.ativado).ThenBy(c => c.nome).Where(x => x.nome.Contains(s) || x.Email.Contains(s) && x.ativado == false).ToList();
                return View("Index", u);
            }
            else
            {
                List<Usuario> u = context.TBUsuario.OrderBy(c => c.ativado).ThenBy(c => c.nome).Where(x => x.ativado == false).ToList();
                return View("Index", u);
            }
        }

        public IActionResult AlterarEmail(int? ID)
        {
            if (ID.HasValue)
            {
                Usuario ss = context.TBUsuario.FirstOrDefault(x => x.ID == ID);

                if (ss != null)
                {
                    return View(ss);
                }
            }

            return NoContent();
        }

        [HttpPost]
        public IActionResult AlterarEmail(Usuario ss)
        {
            Usuario temp = context.TBUsuario.FirstOrDefault(x => x.ID == ss.ID);

            if (ss != null && temp != null)
            {
                temp.Email = ss.Email;
                if (context.TBUsuario.Any(x => x.ID == ss.ID))
                {
                    try
                    {
                        if (temp.previlegios == "administrador")
                            throw new Exception();
                        context.TBUsuario.Update(temp);
                        context.SaveChanges();
                    }
                    catch
                    {
                        TempData["Erro"] = "O Usuario não pode ser atualizado";
                        return View();
                    }

                    TempData["sucesso"] = "Usuario alterado com sucesso";
                    return RedirectToAction("Index", "Admin");
                }
            }

            return NoContent();
        }
    }
}
