using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrabalhoCertificado.Data;
using TrabalhoCertificado.Models;


namespace TrabalhoCertificado.Controllers
{
    public class AtividadeController : Controller
    {
        private readonly DataContext context;

        public AtividadeController(DataContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public ActionResult Index()
        {
            Models.AtividadeLink atividadesLink = new Models.AtividadeLink();

            List<Atividade> atividades = context.TBAtividades.ToList();
            List<TipoAtividade> tipoAtividades = context.TBTiposAtividades.ToList();

            atividadesLink.tipoAtividades = tipoAtividades;
            atividadesLink.atividades = atividades;
            
            
            Usuario usuario = null;
            try
            {
                var sid = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());
                usuario = context.TBUsuario.FirstOrDefault(x => x.ID == sid);
                TempData["id"] = usuario.ID;
                TempData["nome"] = usuario.nome;
            }
            catch
            {
                TempData["erro"] = "Usuario não encontrado!";
            }
        
            
            return View(atividadesLink);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovaAtividade(AtividadeLink item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.TBAtividades.Add(item.atividade);
                    context.SaveChanges();
                }
                catch
                {
                    //erro na hora de salvar
                    ViewBag.MensagemErro = "A atividade não pode ser cadastrado";
                    return View();
                }

                TempData["NovaAtividade"] = true;
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoTipoAtividade(AtividadeLink item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.TBTiposAtividades.Add(item.tipoAtividade);
                    context.SaveChanges();
                }
                catch
                {
                    //erro na hora de salvar
                    ViewBag.MensagemErro = "A atividade não pode ser cadastrado";
                    return View();
                }

                TempData["NovoTipoAtividade"] = true;
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            Atividade atividade = context.TBAtividades.Find(id);
            TipoAtividade tipoAtividade = context.TBTiposAtividades.Find(atividade.idTipoAtiv);
            if (atividade == null)
            {
                return NotFound("Atividade não foi encontrado!");
            }
            if(tipoAtividade == null)
            {
                return NotFound("Tipo de atividade não foi encontrado!");
            }
            AtividadeLink atividadeLink = new AtividadeLink();
            atividadeLink.atividade = atividade;
            atividadeLink.tipoAtividade = tipoAtividade;
            return PartialView(atividadeLink);
        }
        public ActionResult Editar(int? id)
        {   
            if (id == null)
            {
                return new BadRequestResult();
            }
            Atividade atividade = context.TBAtividades.Find(id);
            if (atividade == null)
            {
                return NotFound("Atividade não foi encontrado!");
            }
            TipoAtividade tipoAtividade = context.TBTiposAtividades.Find(atividade.idTipoAtiv);          
            if (tipoAtividade == null)
            {
                return NotFound("Tipo de atividade não foi encontrado!");
            }
            AtividadeLink atividadeLink = new AtividadeLink();
            atividadeLink.atividade = atividade;
            atividadeLink.tipoAtividade = tipoAtividade;
            TempData["id"] = atividade.idUsuario;
            atividadeLink.tipoAtividades = context.TBTiposAtividades.ToList();
            return PartialView(atividadeLink);
        }
        [HttpPost]
        public ActionResult Editar(AtividadeLink item)
        {
           
            if (ModelState.IsValid)
            {
                if (item == null)
                {
                    return new BadRequestResult();
                }
                try
                {
                    context.TBTiposAtividades.Add(item.tipoAtividade);
                    context.SaveChanges();
                }
                catch
                {
                    //erro na hora de salvar
                    ViewBag.MensagemErro = "A atividade não pode ser cadastrado";
                    return View();
                }

                TempData["EditarAtividade"] = true;
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
