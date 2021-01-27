using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrabalhoCertificado.Data;
using Microsoft.AspNetCore.Hosting;
namespace TrabalhoCertificado.Controllers
{
    public class ArquivosController : Controller
    {
        private readonly DataContext context;
        private readonly IHostingEnvironment hostingEnvironment;
        public ArquivosController(DataContext _context, IHostingEnvironment hostingEnviroment)
        {
            context = _context;
            this.hostingEnvironment = hostingEnviroment;
        }

        [HttpGet]
        public IActionResult Index(string parametro)
        {
            if(parametro == null)
            {
                return NoContent();
            }
            Models.Usuario usuario = null;
            try
            {
                var sid = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());
                usuario = context.TBUsuario.FirstOrDefault(x => x.ID == sid);
            }
            catch
            {
                TempData["erro"] = "Usuario não encontrado!";
            }

            var arquivo = context.TBAtividades.FirstOrDefault(x => x.idUsuario == usuario.ID && x.caminhoArquivo == parametro) ;
            if(arquivo == null)
            {
                return NoContent();
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(hostingEnvironment.ContentRootPath + @"/arquivos/" + arquivo.caminhoArquivo);
            string fileName = arquivo.caminhoArquivo;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}
