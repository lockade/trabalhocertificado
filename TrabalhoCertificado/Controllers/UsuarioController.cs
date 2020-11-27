using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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
                    usuario.Senha = sha256encrypt(usuario.Senha);
                    context.TBUsuario.Add(usuario);
                    context.SaveChanges();

                    if(usuario.previlegios == "administrador")
                    {
                        ViewBag.Confirmacao = $"Cadastrado, você ({usuario.Email}) é um administrador";
                    }
                    else
                    {
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

                login.Senha = sha256encrypt(login.Senha);
                List<Usuario> l = (List<Usuario>)context.TBUsuario.Where(x => x.Email == login.Email && x.Senha == login.Senha).ToList();
                if (l != null && l.Count == 1)
                {
                    if (l[0].ativado)
                    {
                        List<Claim> claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Sid, l[0].ID.ToString()),
                            new Claim(ClaimTypes.Name, l[0].nome.ToString()),
                            new Claim(ClaimTypes.Role, l[0].previlegios.ToString()),
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

    }
}
