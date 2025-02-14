﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrabalhoCertificado.Data;
using TrabalhoCertificado.Models;
//O que falta?
//Implemento de busca por formulário!
namespace TrabalhoCertificado.Controllers
{
    [Authorize(Roles = "usuario")]
    public class AtividadeController : Controller
    {
        private readonly DataContext context;
        private readonly IHostingEnvironment hostingEnvironment;

        public AtividadeController(DataContext _context, IHostingEnvironment hostingEnviroment)
        {
            context = _context;
            this.hostingEnvironment = hostingEnviroment;
        }
        //DESCULPA POR ESSE CÓDIGO GIGANTESCO, NÃO TIVE MUITO TEMPO PARA FAZER UM ALGORITMO MAIS TRANQUILO;
        [HttpGet]
        public ActionResult Index(string buscar, string date, string arquivo, string tipoAtividade)
        {
            Models.AtividadeLink atividadesLink = new Models.AtividadeLink();
            List<TipoAtividade> tipoAtividades = context.TBTiposAtividades.ToList();

            if (buscar == null && date == null && arquivo == null && tipoAtividade == null)
            {
                atividadesLink.tipoAtividades = context.TBTiposAtividades.ToList();
                atividadesLink.atividades = context.TBAtividades.ToList();

            }
            else
            {
                DateTime data;
                if (date != null)
                {
                    data = DateTime.Parse(date);
                }
                else
                {
                    data = DateTime.MinValue;
                }

                List<Atividade> atividades = new List<Atividade>();
                TipoAtividade tipo = new TipoAtividade();
                if (tipoAtividade != null)
                    tipo = context.TBTiposAtividades.Find(int.Parse(tipoAtividade));
                else
                    tipo = null;



                foreach (Models.Atividade atividade in context.TBAtividades.ToList())
                {
                    buscar = buscar.ToLower();
                    atividade.nome = atividade.nome.ToLower();
                    if (tipo != null)
                    {
                        if (tipo.ID == atividade.idTipoAtiv)
                        {
                            if (buscar != null)
                            {
                                if (arquivo != null)
                                {
                                    if (atividade.caminhoArquivo != null)
                                    {
                                        if (date != null)
                                        {
                                            if (atividade.DataValidade == null)
                                            {
                                                if (atividade.nome.Contains(buscar) || atividade.dataInicio.ToShortDateString().Contains(data.ToShortDateString())
                                                    || atividade.dataFim.ToShortDateString().Contains(data.ToShortDateString()))
                                                {
                                                    atividades.Add(atividade);
                                                }
                                            }
                                            else
                                            {
                                                if (atividade.nome.Contains(buscar) || atividade.dataInicio.ToShortDateString().Contains(data.ToShortDateString())
                                                || atividade.dataFim.ToShortDateString().Contains(data.ToShortDateString())
                                                || atividade.DataValidade.Value.ToShortDateString().Contains(data.ToShortDateString()))
                                                {
                                                    atividades.Add(atividade);
                                                }

                                            }
                                        }
                                        else
                                        {
                                            if (atividade.nome.Contains(buscar))
                                            {
                                                atividades.Add(atividade);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (date != null)
                                    {
                                        if (atividade.DataValidade == null)
                                        {
                                            if (atividade.nome.Contains(buscar) || atividade.dataInicio.ToShortDateString().Contains(data.ToShortDateString())
                                                || atividade.dataFim.ToShortDateString().Contains(data.ToShortDateString()))
                                            {
                                                atividades.Add(atividade);
                                            }
                                        }
                                        else
                                        {
                                            if (atividade.nome.Contains(buscar) || atividade.dataInicio.ToShortDateString().Contains(data.ToShortDateString())
                                            || atividade.dataFim.ToShortDateString().Contains(data.ToShortDateString())
                                            || atividade.DataValidade.Value.ToShortDateString().Contains(data.ToShortDateString()))
                                            {
                                                atividades.Add(atividade);
                                            }

                                        }
                                    }
                                    else
                                    {
                                        if (atividade.nome.Contains(buscar))
                                        {
                                            atividades.Add(atividade);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (arquivo != null)
                                {
                                    if (atividade.caminhoArquivo != null)
                                    {
                                        if (date != null)
                                        {
                                            if (atividade.DataValidade == null)
                                            {
                                                if (atividade.dataInicio.ToShortDateString().Contains(data.ToShortDateString())
                                                    || atividade.dataFim.ToShortDateString().Contains(data.ToShortDateString()))
                                                {
                                                    atividades.Add(atividade);
                                                }
                                            }
                                            else
                                            {
                                                if (atividade.dataInicio.ToShortDateString().Contains(data.ToShortDateString())
                                                || atividade.dataFim.ToShortDateString().Contains(data.ToShortDateString())
                                                || atividade.DataValidade.Value.ToShortDateString().Contains(data.ToShortDateString()))
                                                {
                                                    atividades.Add(atividade);
                                                }

                                            }
                                        }
                                        else
                                        {
                                            atividades.Add(atividade);
                                        }
                                    }
                                }
                                else
                                {
                                    if (date != null)
                                    {
                                        if (atividade.DataValidade == null)
                                        {
                                            if (atividade.dataInicio.ToShortDateString().Contains(data.ToShortDateString())
                                                || atividade.dataFim.ToShortDateString().Contains(data.ToShortDateString()))
                                            {
                                                atividades.Add(atividade);
                                            }
                                        }
                                        else
                                        {
                                            if (atividade.dataInicio.ToShortDateString().Contains(data.ToShortDateString())
                                            || atividade.dataFim.ToShortDateString().Contains(data.ToShortDateString())
                                            || atividade.DataValidade.Value.ToShortDateString().Contains(data.ToShortDateString()))
                                            {
                                                atividades.Add(atividade);
                                            }

                                        }
                                    }
                                    else
                                    {
                                        atividades.Add(atividade);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (buscar != null)
                        {
                            if (arquivo != null)
                            {
                                if (atividade.caminhoArquivo != null)
                                {
                                    if (date != null)
                                    {
                                        if (atividade.DataValidade == null)
                                        {
                                            if (atividade.nome.Contains(buscar) || atividade.dataInicio.ToShortDateString().Contains(data.ToShortDateString())
                                                || atividade.dataFim.ToShortDateString().Contains(data.ToShortDateString()))
                                            {
                                                atividades.Add(atividade);
                                            }
                                        }
                                        else
                                        {
                                            if (atividade.nome.Contains(buscar) || atividade.dataInicio.ToShortDateString().Contains(data.ToShortDateString())
                                            || atividade.dataFim.ToShortDateString().Contains(data.ToShortDateString())
                                            || atividade.DataValidade.Value.ToShortDateString().Contains(data.ToShortDateString()))
                                            {
                                                atividades.Add(atividade);
                                            }

                                        }
                                    }
                                    else
                                    {
                                        if (atividade.nome.Contains(buscar))
                                        {
                                            atividades.Add(atividade);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (date != null)
                                {
                                    if (atividade.DataValidade == null)
                                    {
                                        if (atividade.nome.Contains(buscar) || atividade.dataInicio.ToShortDateString().Contains(data.ToShortDateString())
                                            || atividade.dataFim.ToShortDateString().Contains(data.ToShortDateString()))
                                        {
                                            atividades.Add(atividade);
                                        }
                                    }
                                    else
                                    {
                                        if (atividade.nome.Contains(buscar) || atividade.dataInicio.ToShortDateString().Contains(data.ToShortDateString())
                                        || atividade.dataFim.ToShortDateString().Contains(data.ToShortDateString())
                                        || atividade.DataValidade.Value.ToShortDateString().Contains(data.ToShortDateString()))
                                        {
                                            atividades.Add(atividade);
                                        }

                                    }
                                }
                                else
                                {
                                    if (atividade.nome.Contains(buscar))
                                    {
                                        atividades.Add(atividade);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (arquivo != null)
                            {
                                if (atividade.caminhoArquivo != null)
                                {
                                    if (date != null)
                                    {
                                        if (atividade.DataValidade == null)
                                        {
                                            if (atividade.dataInicio.ToShortDateString().Contains(data.ToShortDateString())
                                                || atividade.dataFim.ToShortDateString().Contains(data.ToShortDateString()))
                                            {
                                                atividades.Add(atividade);
                                            }
                                        }
                                        else
                                        {
                                            if (atividade.dataInicio.ToShortDateString().Contains(data.ToShortDateString())
                                            || atividade.dataFim.ToShortDateString().Contains(data.ToShortDateString())
                                            || atividade.DataValidade.Value.ToShortDateString().Contains(data.ToShortDateString()))
                                            {
                                                atividades.Add(atividade);
                                            }

                                        }
                                    }
                                    else
                                    {

                                        atividades.Add(atividade);

                                    }
                                }
                            }
                            else
                            {
                                if (date != null)
                                {
                                    if (atividade.DataValidade == null)
                                    {
                                        if (atividade.dataInicio.ToShortDateString().Contains(data.ToShortDateString())
                                            || atividade.dataFim.ToShortDateString().Contains(data.ToShortDateString()))
                                        {
                                            atividades.Add(atividade);
                                        }
                                    }
                                    else
                                    {
                                        if (atividade.dataInicio.ToShortDateString().Contains(data.ToShortDateString())
                                        || atividade.dataFim.ToShortDateString().Contains(data.ToShortDateString())
                                        || atividade.DataValidade.Value.ToShortDateString().Contains(data.ToShortDateString()))
                                        {
                                            atividades.Add(atividade);
                                        }

                                    }
                                }
                                else
                                {
                                    atividades.Add(atividade);
                                }
                            }
                        }
                    }



                    atividadesLink.atividades = atividades;
                    atividadesLink.tipoAtividades = context.TBTiposAtividades.ToList();
                }


            }
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

        public ActionResult BuscarAtividade()
        {
            AtividadeLink atividade = new AtividadeLink();
            atividade.tipoAtividades = context.TBTiposAtividades.ToList();
            var sid = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());
            Usuario usuario = context.TBUsuario.FirstOrDefault(x => x.ID == sid);
            TempData["id"] = usuario.ID;
            return PartialView(atividade);
        }
        public ActionResult NovaAtividade()
        {
            Models.AtividadeLink atividadesLink = new Models.AtividadeLink();
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
            List<Atividade> atividades = context.TBAtividades.ToList();
            List<TipoAtividade> tipoAtividades = context.TBTiposAtividades.Where(a => a.idUsuario == usuario.ID).ToList();

            atividadesLink.tipoAtividades = tipoAtividades;
            atividadesLink.atividades = atividades;


           
            return PartialView(atividadesLink);

        }

        public ActionResult AtividadeDataDeValidade(string buscar)
        {
            Models.AtividadeLink atividadesLink = new Models.AtividadeLink();
            List<Atividade> atividades = new List<Atividade>();
            atividadesLink.tipoAtividades = context.TBTiposAtividades.ToList();
            if (buscar == null)
            {
                foreach (Atividade atividade in context.TBAtividades.ToList())
                {
                    if (atividade.DataValidade != null)
                    {
                        atividades.Add(atividade);
                    }
                }
                atividadesLink.atividades = atividades;
            }
            else
            {
                foreach (Models.Atividade atividade in context.TBAtividades.ToList())
                {
                    TipoAtividade tipo = context.TBTiposAtividades.Find(atividade.idTipoAtiv);
                    if (atividade.DataValidade != null)
                    {
                        if (atividade.nome.Contains(buscar) || atividade.dataInicio.ToShortDateString().Contains(buscar)
                            || atividade.dataFim.ToShortDateString().Contains(buscar) || tipo.NomeAtividade.Contains(buscar))
                        {
                            atividades.Add(atividade);
                        }
                    }
                    atividadesLink.atividades = atividades;
                    atividadesLink.tipoAtividades = context.TBTiposAtividades.ToList();
                }
            }

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

        public ActionResult AtividadePossuemArquivos(string buscar)
        {
            Models.AtividadeLink atividadesLink = new Models.AtividadeLink();

            atividadesLink.tipoAtividades = context.TBTiposAtividades.ToList();
            List<Atividade> atividades = new List<Atividade>();
            if (buscar == null)
            {
                foreach (Atividade atividade in context.TBAtividades.ToList())
                {
                    if (atividade.caminhoArquivo != null)
                    {
                        atividades.Add(atividade);
                    }
                }
                atividadesLink.atividades = atividades;
            }
            else
            {
                foreach (Models.Atividade atividade in context.TBAtividades.ToList())
                {
                    if (atividade.caminhoArquivo != null)
                    {
                        TipoAtividade tipo = context.TBTiposAtividades.Find(atividade.idTipoAtiv);
                        if (atividade.DataValidade == null)
                        {
                            if (atividade.nome.Contains(buscar) || atividade.dataInicio.ToShortDateString().Contains(buscar)
                                || atividade.dataFim.ToShortDateString().Contains(buscar) || tipo.NomeAtividade.Contains(buscar))
                            {
                                atividades.Add(atividade);
                            }
                        }
                        else
                        {
                            if (atividade.nome.Contains(buscar) || atividade.dataInicio.ToShortDateString().Contains(buscar)
                            || atividade.dataFim.ToShortDateString().Contains(buscar)
                            || atividade.DataValidade.Value.ToShortDateString().Contains(buscar)
                            || tipo.NomeAtividade.Contains(buscar))
                            {
                                atividades.Add(atividade);
                            }

                        }
                    }
                }
                atividadesLink.atividades = atividades;
                atividadesLink.tipoAtividades = context.TBTiposAtividades.ToList();
            }
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

            string nomedoArquivo = null;
            if (item == null && item.atividade == null)
            {
                return BadRequest("Atividade não é valida!");
            }
            if (item.atividade.Arquivo != null)
            {
                string[] tiposarquivos = new string[7];

                tiposarquivos[0] = MediaTypeNames.Application.Pdf;
                tiposarquivos[1] = MediaTypeNames.Application.Rtf;
                tiposarquivos[2] = MediaTypeNames.Image.Jpeg;
                tiposarquivos[3] = MediaTypeNames.Image.Tiff;
                tiposarquivos[4] = "image/png";
                tiposarquivos[5] = "image/jpg";
                tiposarquivos[6] = "image/bmp";

                int tipos = 0;
                while (tipos < tiposarquivos.Length)
                {
                    if (tiposarquivos[tipos] == item.atividade.Arquivo.ContentType)
                    {
                        break;
                    }
                    else if (tipos == tiposarquivos.Length)
                    {
                        TempData["erroTiposArquivo"] = true;
                        return RedirectToAction("Index");
                    }
                    tipos++;
                }





                if (item.atividade.Arquivo.Length > 2000000)
                {
                    TempData["erroTamanhoArquivo"] = true;
                    return RedirectToAction("Index");
                }

                string arquivosPasta = Path.Combine(hostingEnvironment.ContentRootPath, "arquivos");
                nomedoArquivo = Guid.NewGuid().ToString() + "_" + item.atividade.Arquivo.FileName;
                string caminhoArquivo = Path.Combine(arquivosPasta, nomedoArquivo);
                FileStream arq = new FileStream(caminhoArquivo, FileMode.Create);
                item.atividade.Arquivo.CopyTo(arq);
                arq.Close();

            }

            item.atividade.caminhoArquivo = nomedoArquivo;

            //item.atividade.caminhoArquivo

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
                foreach (TipoAtividade tipo in context.TBTiposAtividades.ToList())
                {
                    if (tipo.NomeAtividade == item.tipoAtividade.NomeAtividade)
                    {
                        TempData["ErroTipoAtividadeNome"] = "Não é possível adicionar o mesmo nome no tipo de atividade";
                        return RedirectToAction("TiposAtividade");
                    }
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
            if (tipoAtividade == null)
            {
                tipoAtividade = new TipoAtividade(); tipoAtividade.NomeAtividade = "Esse tipo atividade foi excluido!";
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
                tipoAtividade = new TipoAtividade();
                tipoAtividade.NomeAtividade = "Erro 404 - Essa atividade foi deletada ou perdida.";
            }
            AtividadeLink atividadeLink = new AtividadeLink();
            atividadeLink.atividade = atividade;
            atividadeLink.tipoAtividade = tipoAtividade;
            TempData["id"] = atividade.idUsuario;
            atividadeLink.tipoAtividades = context.TBTiposAtividades.ToList();
            return PartialView(atividadeLink);
        }

        [HttpPost]
        public ActionResult DeletandoArquivos(AtividadeLink item)
        {
            Atividade atividade = null;
            if (item == null || item.atividade == null)
            {
                return BadRequest();
            }
            try
            {
                atividade = context.TBAtividades.Find(item.atividade.ID);
            }
            catch
            {
                return NotFound("Não foi possível pesquisar pela atividade");
            }
            if (atividade == null)
            {
                return BadRequest("Atividade não foi localizada");
            }
            string arquivosPasta = Path.Combine(hostingEnvironment.ContentRootPath, "arquivos");
            string arquivo = Path.Combine(arquivosPasta, atividade.caminhoArquivo);
            if (System.IO.File.Exists(arquivo))
            {
                if (System.IO.File.Exists(arquivo))
                {
                    TempData["DeletandoArquivos"] = false;
                    return RedirectToAction("index");
                }
                else
                {
                    atividade.caminhoArquivo = null;
                    try
                    {
                        context.TBAtividades.Update(atividade);
                        context.SaveChanges();
                    }
                    catch
                    {
                        return NotFound("Impossível atualizar a atividade");
                    }
                    TempData["DeletandoArquivos"] = true;
                    return RedirectToAction("index");
                }
            }
            else
            {
                return BadRequest("Aquivo já não existe!");
            }
        }






        [HttpPost]
        public ActionResult Editar(AtividadeLink item)
        {
            //if(item.atividadeArq.MudançaArq == 1 && item.atividadeArq.caminhoArquivo != null)
            //{
            //    return BadRequest("Impossivel enviar duas escolhas ao mesmo tempo");
            //}

            string arquivoAntigo = item.atividade.caminhoArquivo;

            if (ModelState.IsValid)
            {

                if (item == null)
                {
                    return new BadRequestResult();
                }


                string nomedoArquivo = null;


                if (item.atividade.Arquivo != null)
                {

                    string arquivosPasta = Path.Combine(hostingEnvironment.ContentRootPath, "arquivos");
                    if (item.atividade.caminhoArquivo != null)
                    {


                        List<string> tiposArquivos = new List<string>();




                        tiposArquivos.Add(MediaTypeNames.Application.Pdf);
                        tiposArquivos.Add(MediaTypeNames.Application.Rtf);
                        tiposArquivos.Add(MediaTypeNames.Image.Jpeg);
                        tiposArquivos.Add(MediaTypeNames.Image.Tiff);
                        tiposArquivos.Add("image/png");
                        tiposArquivos.Add("image/jpg");
                        tiposArquivos.Add("image/bmp");
                        /*Os tipos de formatos de arquivo de certificado usados podem basear-se em uma combinação de questões de segurança e de compatibilidade. Nesta versão do Windows, você pode importar e exportar certificados nestes formatos:
                       Troca de Informações Pessoais (PKCS #12)
                       Padrão de Sintaxe de Mensagens Criptografadas (PKCS #7)
                       X.509 binário codificado por DER
                       X.509 codificado na Base64

                       Trata-se de um método de codificação desenvolvido para uso com extensões multipropósito do Internet Mail protegidas (S/MIME), que é um método padrão popular para transferência de anexos binários pela Internet.
                       Como todos os clientes compatíveis com MIME podem decodificar arquivos Base64, esse formato pode ser usado por autoridades de certificação que não estão em computadores com o Windows Server 2003, para que seja permitida a interoperabilidade. Os arquivos de certificado na Base64 usam a extensão .cer.*/


                        int tipos = 0;
                        while (tipos <= tiposArquivos.Count)
                        {
                            if (tipos == tiposArquivos.Count)
                            {
                                TempData["erroTiposArquivo"] = true;
                                return RedirectToAction("Index");

                            }
                            else if (tiposArquivos[tipos] == item.atividade.Arquivo.ContentType)
                            {
                                break;
                            }

                            tipos++;
                        }







                        if (item.atividade.Arquivo.Length > 2000000)
                        {
                            TempData["erroTamanhoArquivo"] = true;
                            return RedirectToAction("Index");
                        }
                        //ESSE TAMANHO FOI ESCOLHIDO, POIS UM ARQUIVO PDF DE 2MBS, TEM CERCA DE 30 PÁGINAS. Logo, um "simples" arquivo de atividade, não é necessário passar disso.

                        nomedoArquivo = Guid.NewGuid().ToString() + "_" + item.atividade.Arquivo.FileName;
                        string caminhoArquivo = Path.Combine(arquivosPasta, nomedoArquivo);

                        using (System.IO.Stream a = item.atividade.Arquivo.OpenReadStream())
                        {
                            a.CopyTo(new FileStream(caminhoArquivo, FileMode.CreateNew));
                            a.Close();
                        }


                        item.atividade.caminhoArquivo = nomedoArquivo;

                    }
                    else
                    {
                        nomedoArquivo = Guid.NewGuid().ToString() + "_" + item.atividade.Arquivo.FileName;
                        string caminhoArquivo = Path.Combine(arquivosPasta, nomedoArquivo);

                        using (System.IO.Stream a = item.atividade.Arquivo.OpenReadStream())
                        {
                            a.CopyTo(new FileStream(caminhoArquivo, FileMode.CreateNew));
                            a.Close();
                        }

                        item.atividade.caminhoArquivo = nomedoArquivo;
                    }
                }

                try
                {

                    context.TBAtividades.Update(item.atividade);
                    context.SaveChanges();
                    if (System.IO.File.Exists(arquivoAntigo))
                    {
                        System.IO.File.Delete(arquivoAntigo);
                    }
                }
                catch (Exception e)
                {
                    //erro na hora de salvar
                    return BadRequest("É impossível enviar o arquivo:" + e);
                }

                TempData["EditarAtividade"] = true;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["EditarAtividade"] = false;
                return RedirectToAction("Index");
            }
        }


        public ActionResult RemoverAtividade(int? id)
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

            AtividadeLink atividadeLink = new AtividadeLink();
            atividadeLink.atividade = atividade;
            TempData["id"] = atividade.idUsuario;
            return PartialView(atividadeLink);
        }

        public ActionResult TiposAtividade(string buscar)
        {
            List<TipoAtividade> atividades;
            try
            {
                if (buscar == null)
                {
                    atividades = context.TBTiposAtividades.ToList();

                }
                else
                {
                    atividades = context.TBTiposAtividades.Where(x => x.NomeAtividade.Contains(buscar)).ToList();
                }

            }
            catch
            {
                TempData["ErroTipoAtividades"] = true;
                return RedirectToAction("Index");
            }

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
                return RedirectToAction("Index");
            }


            AtividadeLink atividadeLink = new AtividadeLink();
            atividadeLink.tipoAtividades = atividades;
            return View(atividadeLink);
        }

        [HttpPost]
        public ActionResult RemoverAtividade(AtividadeLink item)
        {

            item.atividade = context.TBAtividades.Find(item.atividade.ID);

            if (item == null || item.atividade == null)
            {
                return new BadRequestResult();
            }
            try
            {

                context.TBAtividades.Remove(item.atividade);
                context.SaveChanges();
            }
            catch
            {
                //erro na hora de deletar
                return NotFound("Não foi possivel deletar a atividade.");
            }

            TempData["deletarAtividade"] = true;
            return RedirectToAction("Index");



        }

        public ActionResult TiposEditar(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            TipoAtividade atividade = context.TBTiposAtividades.Find(id);
            if (atividade == null)
            {
                return NotFound("Atividade não foi encontrado!");
            }

            AtividadeLink atividadeLink = new AtividadeLink();
            atividadeLink.tipoAtividade = atividade;
            TempData["id"] = atividade.idUsuario;
            return PartialView(atividadeLink);
        }

        [HttpPost]
        public ActionResult TiposEditar(AtividadeLink item)
        {

            if (ModelState.IsValid)
            {
                if (item == null)
                {
                    return new BadRequestResult();
                }
                try
                {
                    context.TBTiposAtividades.Update(item.tipoAtividade);
                    context.SaveChanges();
                }
                catch
                {
                    //erro na hora de salvar
                    ViewBag.MensagemErro = "A atividade não pode ser cadastrado";
                }

                TempData["EditarTipoAtividade"] = true;
                return RedirectToAction("TiposAtividade");
            }
            else
            {
                TempData["EditarTiposAtividade"] = false;
                return RedirectToAction("TiposAtividade");
            }
        }

        public ActionResult TiposDetalhes(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            TipoAtividade tipoAtividade = context.TBTiposAtividades.Find(id);
            if (tipoAtividade == null)
            {
                return NotFound("Tipo Atividade não foi encontrado!");
            }
            List<Atividade> atividades = new List<Atividade>();
            try
            {
                foreach (Atividade atividade in context.TBAtividades.ToList())
                {
                    if (atividade.idTipoAtiv == tipoAtividade.ID)
                    {
                        atividades.Add(atividade);
                    }
                }
            }
            catch
            {
                return BadRequest("Não foi possível acessar as atividades");
            }

            Usuario usuario;
            var sid = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());
            usuario = context.TBUsuario.FirstOrDefault(x => x.ID == sid);
            TempData["id"] = usuario.ID;
            TempData["nome"] = usuario.nome;


            AtividadeLink atividadeLink = new AtividadeLink();
            atividadeLink.tipoAtividade = tipoAtividade;
            atividadeLink.atividades = atividades;
            return PartialView(atividadeLink);
        }

        public ActionResult RemoverTipoAtividade(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            TipoAtividade atividade = context.TBTiposAtividades.Find(id);
            if (atividade == null)
            {
                return NotFound("Tipo Atividade não foi encontrado!");
            }

            AtividadeLink atividadeLink = new AtividadeLink();
            atividadeLink.tipoAtividade = atividade;
            TempData["id"] = atividade.idUsuario;
            return PartialView(atividadeLink);
        }

        [HttpPost]
        public ActionResult RemoverTipoAtividade(AtividadeLink item)
        {

            item.tipoAtividade = context.TBTiposAtividades.Find(item.tipoAtividade.ID);

            if (item == null || item.tipoAtividade == null)
            {
                return new BadRequestResult();
            }

            foreach (Atividade atividade in context.TBAtividades.ToList())
            {
                if (atividade.idTipoAtiv == item.tipoAtividade.ID)
                {
                    TempData["deletarTipoAtividade"] = false;
                    TempData["deletarTipoAtividadeTXT"] = "Não foi possível deletar o tipo de atividade, pois existe atividades sincronizadas a ela.";
                    return RedirectToAction("TiposAtividade");
                }
            }
            try
            {

                context.TBTiposAtividades.Remove(item.tipoAtividade);
                context.SaveChanges();
            }
            catch
            {
                //erro na hora de deletar
                return NotFound("Não foi possivel deletar a atividade.");
            }

            TempData["deletarTipoAtividade"] = true;
            return RedirectToAction("TiposAtividade");



        }

        public ActionResult baixarAnexos()
        {
            AtividadeLink atividade = new AtividadeLink();


            Usuario usuario;
            var sid = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());
            usuario = context.TBUsuario.FirstOrDefault(x => x.ID == sid);
            atividade.atividades = context.TBAtividades.Where(a => a.idUsuario == usuario.ID);
            atividade.tipoAtividades = context.TBTiposAtividades.Where(a => a.idUsuario == usuario.ID);
            List<int> anos = new List<int>();

            foreach (Atividade ativ in atividade.atividades)
            {
                if (!anos.Contains(ativ.dataInicio.Year))
                {
                    anos.Add(ativ.dataInicio.Year);
                }
                if (!anos.Contains(ativ.dataFim.Year))
                {
                    anos.Add(ativ.dataFim.Year);
                }
                if (ativ.DataValidade.HasValue)
                {
                    if (!anos.Contains(ativ.DataValidade.Value.Year))
                    {
                        anos.Add(ativ.DataValidade.Value.Year);
                    }
                }
            }

            atividade.anos = anos;
            return PartialView(atividade);
        }
        [HttpPost]
        public ActionResult baixarAnexos(AtividadeLink atividadeLink, string tipoAtividade)
        {
            TipoAtividade tipo = context.TBTiposAtividades.Find(int.Parse(tipoAtividade));

            List<Atividade> atividade1 = context.TBAtividades.Where(a => a.idTipoAtiv == tipo.ID && a.caminhoArquivo != null && a.dataInicio.Year == atividadeLink.ano ||
            a.dataFim.Year == atividadeLink.ano || a.DataValidade.Value.Year == atividadeLink.ano).ToList();
            Usuario usuario;
            var sid = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());
            usuario = context.TBUsuario.FirstOrDefault(x => x.ID == sid);
            List<Atividade> atividades = new List<Atividade>();
            foreach (Atividade ativ in atividade1)
            {
                if(ativ.caminhoArquivo != null)
                {
                    if (ativ.idUsuario == usuario.ID)
                        atividades.Add(ativ);
                }
            }
            List<string> arquivosCaminhos = new List<string>();

            foreach (Atividade atividade in atividades)
            {
                arquivosCaminhos.Add(hostingEnvironment.ContentRootPath + @"\arquivos\" + atividade.caminhoArquivo);
            }

            string nomedoArquivo = "AtividadesAnexos.zip";
            string caminhoDoArquivo = hostingEnvironment.ContentRootPath + @"\arquivos\" + nomedoArquivo;

            using (ZipOutputStream zipout = new ZipOutputStream(System.IO.File.Create(caminhoDoArquivo)))
            {
                zipout.SetLevel(9);
                byte[] buffer = new byte[4096];
                foreach (string caminho in arquivosCaminhos)
                {
                    ZipEntry entry = new ZipEntry(Path.GetFileName(caminho));
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    zipout.PutNextEntry(entry);
                    using (FileStream arquivo = System.IO.File.OpenRead(caminho))
                    {
                        int bytesPadroes;
                        do
                        {
                            bytesPadroes = arquivo.Read(buffer, 0, buffer.Length);
                            zipout.Write(buffer, 0, bytesPadroes);
                        } while (bytesPadroes > 0);
                    }
                }
                zipout.Finish();
                zipout.Flush();
                zipout.Close();
            }
            byte[] resultadoFinal = System.IO.File.ReadAllBytes(caminhoDoArquivo);
            if (System.IO.File.Exists(caminhoDoArquivo))
            {
                System.IO.File.Delete(caminhoDoArquivo);
            }
            if(resultadoFinal == null)
            {
                return BadRequest();
            }

            return File(resultadoFinal, "application/zip", nomedoArquivo);




            //return null;
            //byte[] fileBytes = System.IO.File.ReadAllBytes(hostingEnvironment.ContentRootPath + @"/arquivos/" + arquivo.caminhoArquivo);
            //string fileName = arquivo.caminhoArquivo;
            //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}


        

    





