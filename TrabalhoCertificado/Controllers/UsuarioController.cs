using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            if (User.Identity.IsAuthenticated && User.IsInRole("usuario"))
                return RedirectToAction("Index", "Home");
            if (User.Identity.IsAuthenticated && User.IsInRole("administrador"))
                return RedirectToAction("Index", "Admin");

            return View();
        }


        public IActionResult AlterarSenha(string ID)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("usuario"))
                return RedirectToAction("Index", "Home");
            if (User.Identity.IsAuthenticated && User.IsInRole("administrador"))
                return RedirectToAction("Index", "Admin");

            if(ID != null)
            {
                //localizar no banco e renderizar a alteração
                RecuperarSenhaLinks s = context.TBRecuperarSenhaLinks.FirstOrDefault(x => x.IDEncry == ID);
                if(s != null)
                {
                    //existe no banco
                    TimeSpan diff = DateTime.Now - s.tempo;

                    if (!(diff.TotalSeconds <= 3600))
                        ViewBag.TempoEx = "Tempo Expirado!";
                }


                return View(s);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }




        [HttpPost]
        public IActionResult AlterarSenha(RecuperarSenhaLinks r)
        {
            if (ModelState.IsValid)
            {
                RecuperarSenhaLinks temp = context.TBRecuperarSenhaLinks.FirstOrDefault(x => x.IDEncry == r.IDEncry);
                if (temp != null)
                {
                    Usuario u = context.TBUsuario.FirstOrDefault(x => x.Email == temp.Email);
                    u.senhaEncry = sha256encrypt(r.Senha);
                    context.TBUsuario.Update(u);
                    context.TBRecuperarSenhaLinks.Remove(temp);
                    ViewBag.Confirmacao = "Senha Alterada";
                    context.SaveChanges();

                }
                else
                {
                    ViewBag.Confirmacao = "Erro";
                }


            }
            return View();
        }

        public IActionResult RecuperarSenha()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("usuario"))
                return RedirectToAction("Index", "Home");
            if (User.Identity.IsAuthenticated && User.IsInRole("administrador"))
                return RedirectToAction("Index", "Admin");

            return View();
        }


        [HttpPost]
        public IActionResult RecuperarSenha(RecuperarSenhaLinks usuario)
        {
            if(usuario.Email == "")
            {
                ViewBag.Erro = "Email vazio.";
                return View();
            }
            Usuario u = context.TBUsuario.FirstOrDefault(x => x.Email == usuario.Email);

            if (u != null)
            {
                RecuperarSenhaLinks s = new RecuperarSenhaLinks();
                s.IDEncry = sha256encrypt("GustavoJeffersonNatasha" + u.ID);
                s.tempo = DateTime.Now;
                s.Email = u.Email;
                RecuperarSenhaLinks temp = context.TBRecuperarSenhaLinks.FirstOrDefault(x => x.IDEncry == s.IDEncry);
                if (temp != null)
                    context.TBRecuperarSenhaLinks.Remove(temp);
                context.TBRecuperarSenhaLinks.Add(s);
                context.SaveChanges();

                string link = "<a href=\"" + "https://" + HttpContext.Request.Host + "/Usuario/AlterarSenha/" + s.IDEncry + "\">Alterar Senha</a>";
                SendMail(u.Email, "Link para recuperação: " + link, "Recuperação de Senha - Certificados");
                ViewBag.Confirmacao = "Um email foi enviado com um link para alteração da senha (validade de 1 hora)";
            }
            else
            {
                ViewBag.Erro = "Email não encontrado";
            }

            return View();
        }

        



        public IActionResult Cadastro()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("usuario"))
                return RedirectToAction("Index", "Home");
            if (User.Identity.IsAuthenticated && User.IsInRole("administrador"))
                return RedirectToAction("Index", "Admin");

            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario usuario)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                try
                {
                    usuario.ativado = false;
                    List<Usuario> a = (List<Usuario>)context.TBUsuario.ToList();
                    if(a.Count == 0)
                    {
                        usuario.previlegios = "administrador";
                        usuario.ativado = true;
                    }
                    else
                    {
                        usuario.previlegios = "usuario";
                    }
                    usuario.senhaEncry = sha256encrypt(usuario.Senha);
                    context.TBUsuario.Add(usuario);
                    context.SaveChanges();

                    if(usuario.previlegios == "administrador")
                    {
                        ViewBag.Confirmacao = $"Cadastrado, você ({usuario.Email}) é um administrador";
                    }
                    else
                    {
                        SendMail(usuario.Email, "Seu cadastro foi efetuado com sucesso, aguarde o administrador ativa-lo", "Cadastro Efetuado");
                        ViewBag.Confirmacao = "Cadastrado, aguarde aprovação do administrador, será enviado um email quando aprovado";
                    }

                    //fazer a inserção do primeiro usuario ser o admin
                }
                catch (Exception e)
                {
                    if (e.HResult.ToString() == "-2146233088")
                    {
                        ViewBag.Erro = "Email já cadastrado.";
                    }
                    else
                    {
                        ViewBag.Erro = "Erro no cadastro.";
                    }

                    
                }
                
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


        private void AlterarClaim(string id, string nome, string previ)
        {
            List<Claim> claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Sid, id),
                            new Claim(ClaimTypes.Name, nome),
                            new Claim(ClaimTypes.Role, previ),
                        };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = true,
            };

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }
        

        [HttpPost]
        public IActionResult Index(Usuario login)
        {

            if (login.Email != null && login.Senha != null)
            {
                login.Email = login.Email.Trim();
                login.Senha = login.Senha.Trim();

                login.senhaEncry = sha256encrypt(login.Senha);
                Usuario l = context.TBUsuario.FirstOrDefault(x => x.Email == login.Email && x.senhaEncry == login.senhaEncry);
                if (l != null)
                {
                    if (l.ativado)
                    {

                        AlterarClaim(l.ID.ToString(), l.nome.ToString(), l.previlegios.ToString());
                        if (l.previlegios == "administrador")
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    
                    ViewBag.Erro = "Usuário não ativado.";
                } 
                else
                {
                    ViewBag.Erro = "Usuário e/ou senha inválidos.";
                }
            }
            else
            {
                ViewBag.Erro = "Campos não preenchidos.";
            }
            
            return View();

        }

        
        [Authorize(Roles = "administrador")]
        public ActionResult Ativar(int? id)
        {
            if(id.HasValue)
            {
                Usuario u = context.TBUsuario.FirstOrDefault(x => x.ID == id);

                if(u != null)
                {
                    try
                    {
                        if (u.previlegios == "administrador")
                            throw new Exception("Administrador não pode ser alterado");
                        u.ativado = true;
                        context.Update(u);
                        context.SaveChanges();
                        TempData["sucesso"] = "Usuario Ativado com sucesso";

                        SendMail(u.Email, "Seu cadastro foi ativado em nosso site!", "Ativação de Cadastro");
                        return RedirectToAction("Index", "Admin");
                    }
                    catch (Exception)
                    {
                        TempData["erro"] = "Usuario não Ativado";
                        return RedirectToAction("Index", "Admin");
                    }
                }
            }
            return NoContent();
        }

        [Authorize(Roles = "administrador")]
        public ActionResult Desativar(int? id)
        {
            if (id.HasValue)
            {
                Usuario u = context.TBUsuario.FirstOrDefault(x => x.ID == id);

                if (u != null)
                {
                    try
                    {
                        if (u.previlegios == "administrador")
                            throw new Exception("Administrador não pode ser alterado");
                        u.ativado = false;
                        context.Update(u);
                        context.SaveChanges();
                        TempData["sucesso"] = "Usuario Desativado com sucesso";

                        SendMail(u.Email, "Seu cadastro foi desativado em nosso site, voce não pode ser acesso!", "Ativação de Cadastro");
                        return RedirectToAction("Index", "Admin");
                    }
                    catch (Exception)
                    {
                        TempData["erro"] = "Usuario não Desativado";
                        return RedirectToAction("Index", "Admin");
                    }
                }
            }
            return NoContent();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AlterarNome(Usuario u)
        {
            Usuario c = null;
            try
            {
                var sid = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());

                c = context.TBUsuario.FirstOrDefault(x => x.ID == sid);

                if (u.ID != c.ID)
                    return NoContent();

                c.nome = u.nome;
                context.TBUsuario.Update(c);
                context.SaveChanges();

                TempData["success"] = "Nome alterado com sucesso";
            }
            catch (Exception)
            {
                TempData["erro"] = "Nome não alterado";
            }

            if (c != null)
                AlterarClaim(c.ID.ToString(), c.nome, c.previlegios);


            return RedirectToAction("perfil", "Home");
        }

        [Authorize]
        [HttpPost]
        public ActionResult AlterarSenhaUser(Usuario u)
        {
            Usuario c = null;
            try
            {
                var sid = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());
                string senhaC = sha256encrypt(u.Senha);
                u.senhaEncry = sha256encrypt(u.senhaEncry);
                c = context.TBUsuario.FirstOrDefault(x => x.ID == sid && u.Email == x.Email && x.senhaEncry == u.senhaEncry);

                if (c == null)
                    throw new Exception();
                if (u.ID != c.ID)
                    throw new Exception();

                if (ModelState.GetFieldValidationState("Senha") != ModelValidationState.Valid)
                {
                    TempData["erro"] = "Nova senha deve possuir no mínimo 8 caracteres, ao menos 1 letra maíuscula, 1 letra minúscula e 1 caractere especial";
                    return RedirectToAction("AlterarSenhaUser", "Home");
                }


                c.senhaEncry = senhaC;
                context.TBUsuario.Update(c);
                context.SaveChanges();

                TempData["success"] = "Senha alterada com sucesso";
            }
            catch (Exception)
            {
                TempData["erro"] = "Senha não alterada";
            }


            return RedirectToAction("AlterarSenhaUser", "Home");
        }

        [Authorize(Roles = "usuario")]
        [HttpPost]
        public ActionResult RemoverConta(Usuario u)
        {
            Usuario c = null;
            try
            {
                var sid = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());
                u.senhaEncry = sha256encrypt(u.senhaEncry);
                c = context.TBUsuario.FirstOrDefault(x => x.ID == sid && u.Email == x.Email && x.senhaEncry == u.senhaEncry);

                if (c == null)
                    throw new Exception();
                if (u.ID != c.ID)
                    throw new Exception();


                //DEVE-SE REMOVER TODOS OS DADOS TAMBEM

                context.TBUsuario.Remove(c);
                context.SaveChanges();

                TempData["success"] = "Conta Excluida";
            }
            catch (Exception)
            {
                TempData["erro"] = "Conta não excluida, senha inválida";
                return RedirectToAction("RemoverConta", "Home");
            }

            //deslogar
            HttpContext.SignOutAsync();


            return RedirectToAction("Index", "Home");
        }

        public static string sha256encrypt(string frase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(frase));
            //return BitConverter.ToString(hashedDataBytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashedDataBytes.Length; i++)
            {
                builder.Append(hashedDataBytes[i].ToString("x2"));
            }
            return builder.ToString();
        }



        public bool SendMail(string email, string texto, string titulo)
        {
            try
            {
                // Estancia da Classe de Mensagem
                MailMessage _mailMessage = new MailMessage();
                // Remetente
                _mailMessage.From = new MailAddress("trabalhocertificados@gmail.com");

                // Destinatario seta no metodo abaixo

                //Contrói o MailMessage
                _mailMessage.CC.Add(email);
                _mailMessage.Subject = titulo;
                _mailMessage.IsBodyHtml = true;
                _mailMessage.Body = texto;

                //CONFIGURAÇÃO COM PORTA
                SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

                //CONFIGURAÇÃO SEM PORTA
                // SmtpClient _smtpClient = new SmtpClient(UtilRsource.ConfigSmtp);

                // Credencial para envio por SMTP Seguro (Quando o servidor exige autenticação)
                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new NetworkCredential("trabalhocertificados@gmail.com", "trabalho@!#");

                _smtpClient.EnableSsl = true;

                _smtpClient.Send(_mailMessage);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
